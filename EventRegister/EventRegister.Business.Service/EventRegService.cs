using EventRegister.Business.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventRegister.Business.RepoContract.Model;
using EventRegister.Business.RepoContract;

namespace EventRegister.Business.Service
{
    public sealed class EventRegService : IEventRegService
    {
        IEventRegRepo repo;
        public EventRegService(IEventRegRepo _repo)
        {
            repo = _repo;
        }

        public void ValidateRegister(ref RegisterReq req)
        {
            //validate event email not already there
            if (IsAlreadyRegistered(req))
            {
                req.IsError = true;
                req.Error = "The email entered has already been registered to this event";
            }            
            //validate arrival is on time
            if (IsTooLate(req))
            {
                req.IsError = true;
                req.Error = "The arrival time booked is after the event finishes";
            }
        }

        private bool IsTooLate(RegisterReq req)
        {
            /*This is assuming that the times are held in the database at Uk time always.
             * It also assumes that the frontend code will be display local time always. 
             * local time for frontend would be given from server... if i had time to do that api endpoint 
             * for a GET registration page... I would pass it a converted local time for the event they're picking.
             */

            string eventName = req.EventName;
            string timeZoneName = repo.GetAllEvents()
                .FirstOrDefault(e => e.Name == req.EventName && e.Location.Country.CountryCode == req.Country)                
                .Location.LocationTimezone.Name;

            DateTime ukEndTime = repo.GetAllEvents()
                .FirstOrDefault(e => e.Name == req.EventName)
                .UkEndTime;

            DateTime localEndTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(ukEndTime, timeZoneName);

            return (req.ArrivalTime > localEndTime);
        }

        private bool IsAlreadyRegistered(RegisterReq req)
        {
            string emailAddress = req.EmailAddress;
            return repo.GetAllEvents()
                .FirstOrDefault(e => e.Name == req.EventName && e.Location.Country.CountryCode == req.Country).RegisteredEmails
                .Any(em => em == emailAddress);
        }
    }
}
