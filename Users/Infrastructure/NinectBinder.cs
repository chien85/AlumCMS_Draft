using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ninject;
namespace Users.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel ikernel;
        public NinjectDependencyResolver(IKernel kernelpara)
        {
            ikernel = kernelpara;
            addbinding();

        }



        public object GetService(Type serviceType)
        {
            return ikernel.TryGet(serviceType);

            //throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ikernel.GetAll(serviceType);
            // throw new NotImplementedException();
        }


        public void addbinding()
        {
            //bind the interface IcontentRepo to EntityRepo datacontext
            ikernel.Bind<IRepo>().To<Repo>();




        }
    }
}