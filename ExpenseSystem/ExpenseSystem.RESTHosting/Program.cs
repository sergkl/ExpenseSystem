using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseSystem.RESTService;
using System.ServiceModel.Web;

namespace ExpenseSystem.RESTHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            RestDemoServices restDemoServices = new RestDemoServices();
            WebServiceHost webServiceHost = new WebServiceHost(restDemoServices, new Uri("http://localhost:8000/DEMOService"));
            webServiceHost.Open();
            Console.ReadKey();
            webServiceHost.Close();
        }
    }
}
