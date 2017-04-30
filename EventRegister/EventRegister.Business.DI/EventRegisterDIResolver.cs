using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.Web.Mvc;
using EventRegister.Business.Sql;
using EventRegister.Business.RepoContract;
using EventRegister.Business.Repo;
using EventRegister.Business.ServiceContract;
using EventRegister.Business.Service;

namespace EventRegister.Business.DI
{
    public class EventRegisterDIResolver : IDependencyResolver
    {
        private IKernel kernel;

        public EventRegisterDIResolver(IKernel _kernel)
        {
            kernel = _kernel;
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<IEventRegContext>().To<EventRegContext>();
            kernel.Bind<IEventRegRepo>().To<EventRegRepo>();
            kernel.Bind<IEventRegService>().To<EventRegService>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}
