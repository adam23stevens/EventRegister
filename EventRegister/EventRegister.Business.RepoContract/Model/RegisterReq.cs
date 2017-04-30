using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegister.Business.RepoContract.Model
{
    public class RegisterReq : BaseRequest
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string EventName { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime RegistrationDate { get; set; }        
    }
}
