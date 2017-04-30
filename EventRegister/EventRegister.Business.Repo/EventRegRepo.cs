using EventRegister.Business.RepoContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventRegister.Business.Sql;
using EventRegister.Business.RepoContract.Model;
using System.Data.Entity;
using EventRegister.Business.ServiceContract;

namespace EventRegister.Business.Repo
{
    public sealed class EventRegRepo : IEventRegRepo
    {
        private IEventRegContext ctx;
        private IEventRegService service;
        public EventRegRepo(IEventRegContext _ctx, IEventRegService _service)
        {
            ctx = _ctx;
            service = _service;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return ctx.Events;
        }
        
        public async Task<IEnumerable<string>> GetRegisterdEmailsForEvent(EventReq req)
        {
            List<string> emails = new List<string>();
            await ctx.Events.Where(e => e.Name == req.EventName).ForEachAsync(e => emails.AddRange(e.RegisteredEmails));

            return emails;
        }

        public void RegisterToEvent(RegisterReq req)
        {
            /*This is bound to be common validation for a lot of RESTful requests. 
             *I would much rather it be in a custom filter if I had the time*/
            if (ctx.Events.Any(e => e.Name == req.EventName))
            {
                req.IsError = true;
                req.Error = "This event does not exist";
            }

            //far less common validation so give it to a service
            service.ValidateRegister(ref req);

            if (req.IsError)
            {
                throw new Exception(req.Error);
            }
            else
            {
                try
                {
                    ctx.BeginTransaction();
                    ctx.Events.FirstOrDefault(e => e.Name == req.EventName 
                                            && e.Location.Country.CountryCode == req.Country)
                                            .RegisteredEmails.Add(req.EmailAddress);
                    ctx.SetModified(ctx.Events);
                    ctx.CommitTransaction();
                }
                catch
                {
                    ctx.RollbackTransaction();
                }
            }
        }

    }
}
