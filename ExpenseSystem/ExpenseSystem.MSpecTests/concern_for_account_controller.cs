using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Moq;
using ExpenseSystem.Controllers;
using System.Web.Mvc;
using System.Web;
using ExpenseSystem.Repositories.Interfaces;
using System.IO;
using System.Web.SessionState;
using System.Reflection;

namespace ExpenseSystem.MSpecTests
{
    public class concern_for_account_controller
    {
        Establish context = () =>
        {
            mockController = new Mock<AccountController>();
            var mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            var mockResponse = new Mock<HttpResponseBase>();
            var mockRequest = new Mock<HttpRequestBase>();
            
            mockUserRepository = new Mock<IUserRepository>();
            mockResponse.Setup(p => p.Cookies).Returns(new HttpCookieCollection());
            mockControllerContext.Setup(p => p.HttpContext.Request).Returns(mockRequest.Object);
            mockControllerContext.Setup(p => p.HttpContext.Response).Returns(mockResponse.Object);
            mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            HttpContext.Current = CreateHttpContext("index.aspx", "http://tempuri.org/index.aspx", null);
            controller = mockController.Object;
            controller.ControllerContext = mockControllerContext.Object;
        };

        protected static Mock<AccountController> mockController;
        protected static Mock<IUserRepository> mockUserRepository;
        protected static Mock<ITagRepository> mockTagRepository;
        protected static ActionResult result;
        protected static AccountController controller;

        private static HttpContext CreateHttpContext(string fileName, string url, string queryString)
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var httpResponse = new HttpResponse(sw);
            var httpRequest = new HttpRequest(fileName, url, queryString);
            var httpContext = new HttpContext(httpRequest, httpResponse);
            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                                 new HttpStaticObjectsCollection(), 10, true,
                                                                 HttpCookieMode.AutoDetect,
                                                                 SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                                     BindingFlags.NonPublic | BindingFlags.Instance,
                                                     null, CallingConventions.Standard,
                                                     new[] { typeof(HttpSessionStateContainer) },
                                                     null)
                                                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }
    }
}
