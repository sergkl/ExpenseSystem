﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ExpenseSystem.RESTService
{
    [ServiceContract(Name = "RESTDemoServices")]
    public interface IRESTDemoServices
    {
        [OperationContract]
        [WebGet(UriTemplate = Routing.GetClientRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetUserFullName(string Id);
    }
}
