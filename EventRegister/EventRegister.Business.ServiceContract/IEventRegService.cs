using EventRegister.Business.RepoContract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegister.Business.ServiceContract
{
    public interface IEventRegService
    {
        void ValidateRegister(ref RegisterReq req);
    }
}
