using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Machine.Specifications;
using Moq;

namespace ExpenseSystem.Behaviour
{
    /// <summary>
    /// Class helps us to work with the very top level for establishing for test,
    /// such as controller context and current http context.
    /// </summary>
    public abstract class concern_controller
    {
        /// <summary>
        /// Establish for test
        /// </summary>
        Establish context = () =>
        {
            //Mocking http current context and controller context
            HttpContext.Current = CreateHttpContext("index.aspx", "http://tempuri.org/index.aspx", null);
            mockControllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            var mockResponse = new Mock<HttpResponseBase>();
            var mockRequest = new Mock<HttpRequestBase>();
            var userMock = new Mock<IPrincipal>();
            var contextMock = new Mock<HttpContextBase>();

            //Create use mockup
            userMock.Setup(p => p.Identity.IsAuthenticated).Returns(true);

            //Mock HttpContextBase
            mockResponse.Setup(p => p.Cookies).Returns(new HttpCookieCollection());
            contextMock.Setup(p => p.Request).Returns(mockRequest.Object);
            contextMock.Setup(p => p.Response).Returns(mockResponse.Object);
            contextMock.Setup(p => p.Session).Returns(mockSession.Object);
            contextMock.SetupGet(ctx => ctx.User).Returns(userMock.Object);
            mockControllerContext.SetupGet(con => con.HttpContext).Returns(contextMock.Object);
        };

        /// <summary>
        /// Method creates http context
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="url">URL for file</param>
        /// <param name="queryString">Query string for context</param>
        /// <returns>It returns object of HttpContext class</returns>
        public static HttpContext CreateHttpContext(string fileName, string url, string queryString)
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

        /// <summary>
        /// Mock object for controller context
        /// </summary>
        protected static Mock<ControllerContext> mockControllerContext;
    }


}
