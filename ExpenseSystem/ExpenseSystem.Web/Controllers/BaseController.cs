using System.Web.Mvc;
using ExpenseSystem.Helpers;

namespace ExpenseSystem.Controllers
{
    /// <summary>
    /// Custom parent for controller
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Object which helps working with sessions
        /// </summary>
        public SessionHelper SessionVars { get; set; }

        /// <summary>
        /// Base controller, constructor: initialize sessions and user name
        /// </summary>
        protected BaseController()
        {
            SessionVars = new SessionHelper();
            ViewData["UserName"] = SessionVars.UserName;
        }
    }
}