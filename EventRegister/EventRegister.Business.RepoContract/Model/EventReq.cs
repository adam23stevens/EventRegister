using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegister.Business.RepoContract.Model
{
    public class EventReq : BaseRequest
    {
        public string EventName { get; set; }
    }
}
