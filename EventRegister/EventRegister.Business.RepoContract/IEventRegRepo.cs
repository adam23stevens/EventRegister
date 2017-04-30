using EventRegister.Business.RepoContract.Model;
using EventRegister.Business.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegister.Business.RepoContract
{
    public interface IEventRegRepo
    {
        void RegisterToEvent(RegisterReq req);
        Task<IEnumerable<string>> GetRegisterdEmailsForEvent(EventReq req);
        IEnumerable<Event> GetAllEvents();
    }
}
