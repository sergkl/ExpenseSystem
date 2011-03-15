using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace ExpenseSystem.Controllers
{
    /// <summary>
    /// Custom controller factory to help us integrate unity container
    /// </summary>
    public class UnityControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Unity container
        /// </summary>
        IUnityContainer _container;

        /// <summary>
        /// Constructor for custom controller factory to initialize the unity container
        /// </summary>
        /// <param name="container"></param>
        public UnityControllerFactory(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Overriden method to getcontroller type.
        /// </summary>
        /// <param name="requestContext">Request controller</param>
        /// <param name="controllerType">Type of controller</param>
        /// <returns>Controller object</returns>
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