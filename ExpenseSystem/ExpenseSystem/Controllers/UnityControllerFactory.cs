using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Web.Routing;

namespace ExpenseSystem.Controllers
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        IUnityContainer _container;

        public UnityControllerFactory(IUnityContainer container)
        {
            this._container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            return (IController)_container.Resolve(controllerType);
        }
    }
}