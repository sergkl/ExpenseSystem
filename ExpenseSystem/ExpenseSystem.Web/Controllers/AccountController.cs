using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ExpenseSystem.Common;
using ExpenseSystem.Entities;
using ExpenseSystem.Extensions;
using ExpenseSystem.Models;
using ExpenseSystem.Repositories;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.ViewModels.Account;
using Microsoft.Practices.Unity;
using Dto = ExpenseSystem.Entities;

namespace ExpenseSystem.Controllers
{
    /// <summary>
    /// Controller helps working with account
    /// </summary>
    public class AccountController : BaseController
    {
        /// <summary>
        /// Repository for user
        /// </summary>
        [Dependency]
        public IUserRepository UserRepository { get; set; }

        /// <summary>
        /// Repository for tag
        /// </summary>
        [Dependency]
        public ITagRepository TagRepository { get; set; }

        /// <summary>
        /// Action uses to return view with empty log on form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// Action authenticate user in the Expense system and redirects user to home page
        /// </summary>
        /// <param name="logOnViewModel">Data for authentication</param>
        /// <param name="returnUrl">This url uses as return url, when user will be authenticated</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Dto.User user = UserRepository.GetUserByCredentials(logOnViewModel.Login, logOnViewModel.Password).Object;
                if (user != null)
                {
                    SessionVars.UserId = user.Id;
                    SessionVars.UserName = string.Format("{0} {1} {2}", user.FirstName, user.MiddleName, user.LastName).Replace("  ", " "); //The last replacing will work when user don't have middle name. It will correct full name format.
                    var ticket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddSeconds(1800), false, "SimpleUser"); //TODO: Implement more rolles if it will be needed for the task.

                    var strEncryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strEncryptedTicket);
                    Response.Cookies.Add(cookie);

                    // Redirect back to the page you were trying to access
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("GetTagsTree", "Tag");
                    }
                }
                else
                {
                    ModelState.AddModelError("Login", "Credentials are wrong");
                }
            }
            return View(logOnViewModel);
        }

        /// <summary>
        /// Starting registration process
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Action register user in Expense system
        /// </summary>
        /// <param name="registrationViewModel">Registration model</param>
        /// <returns>If success user redirects to home page, otherwise to the registation page with description of errors</returns>
        [HttpPost]
        public ActionResult Registration(RegistrationViewModel registrationViewModel)
        {
            if (UserRepository.IsLoginExists(registrationViewModel.Login))
            {
                ModelState.AddModelError("Login", Error.CurrentLoginExists);
            }

            if (!string.Equals(registrationViewModel.Password, registrationViewModel.ConfirmPassword))
            {
                ModelState.AddModelError("Password", Error.PasswordsMismatch);
            }

            if (ModelState.IsValid)
            {
                Dto.User user = registrationViewModel.ToEntity();

                //Save / Register user
                AddResponse response = UserRepository.Add(SessionVars.UserId, user);
                user.Id = response.Id;

                TagRepository.Add(user.Id, new Tag() { Name = "ExpensesTag" });
                var ticket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddSeconds(1800), false, "SimpleUser"); //TODO: Implement more rolles if it will be needed for the task, or I could user MemberShip provide. Just wanted to show my experience with authentication.
                var strEncryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strEncryptedTicket);
                Response.Cookies.Add(cookie);

                SessionVars.UserId = user.Id;
                SessionVars.UserName = string.Format("{0} {1} {2}", user.FirstName, user.MiddleName, user.LastName).Replace("  ", " "); //The last replacing will work when user don't have middle name. It will correct full name format.
                return RedirectToAction("GetTagsTree", "Tag");
            }
            else
            {
                return View(registrationViewModel);
            }
        }

        /// <summary>
        /// Sing out user
        /// </summary>
        public ActionResult SignOut()
        {
            SessionVars.Clear();
            FormsAuthentication.SignOut();
            return Redirect("~/");
        }
    }

    public class UserAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Account/LogOn?ReturnUrl=");             
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class PreventCSRF : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string sessionId = filterContext.RequestContext.HttpContext.Request.Params["sessionId"];
            if (sessionId != null && sessionId == filterContext.HttpContext.Session.SessionID)
            {
                base.OnActionExecuting(filterContext);
            }
            else if (!filterContext.HttpContext.Response.IsRequestBeingRedirected)
            {
                filterContext.Result = new RedirectResult("/Account/LogOn?ReturnUrl=");
            }
        }
    }
}
