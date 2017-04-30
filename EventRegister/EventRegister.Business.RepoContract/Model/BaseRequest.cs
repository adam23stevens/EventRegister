using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegister.Business.RepoContract.Model
{
    public class BaseRequest
    {
        public string Error { get; set; }
        public bool IsError { get; set; }

        public BaseRequest()
        {
            IsError = false;
        }
    }
}
