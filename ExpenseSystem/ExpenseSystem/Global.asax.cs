using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using ExpenseSystem.Controllers;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories;
using ExpenseSystem.Model;

namespace ExpenseSystem
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        private static void RegisterTypes(IUnityContainer container)
        {
            var injectedContext = new InjectionConstructor(new ExpenseSystemEntities());
            container.RegisterType<IExpenseRecordRepository, ExpenseRecordRepository>(injectedContext);
            container.RegisterType<ITagRepository, TagRepository>(injectedContext);
            container.RegisterType<IUserRepository, UserRepository>(injectedContext);
        }

        private static void RegisterControllerFactory(IUnityContainer container)
        {
            var factory = new UnityControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

        protected void Application_Start()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            RegisterControllerFactory(container);

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}