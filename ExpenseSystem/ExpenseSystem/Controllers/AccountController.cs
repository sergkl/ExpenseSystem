using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ExpenseSystem.Models;
using ExpenseSystem.ViewModels.Account;
using ExpenseSystem.Extensions;
using Dto = ExpenseSystem.Entities;
using ExpenseSystem.Repositories;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Entities;

namespace ExpenseSystem.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserRepository userRepository = new UserRepository(Context);
                Dto.User user = userRepository.GetUserByCredentials(logOnViewModel.Login, logOnViewModel.Password);
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
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Login", "Credentials are wrong");
                }
            }
            return View(logOnViewModel);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel registrationViewModel)
        {
            UserRepository userRepository = new UserRepository(Context);

            if (userRepository.IsLoginExists(registrationViewModel.Login))
            {
                ModelState.AddModelError("Login", "Current login is busy. Choose another one");
            }

            if (!string.Equals(registrationViewModel.Password, registrationViewModel.ConfirmPassword))
            {
                ModelState.AddModelError("Password", "Password and confirm. password aren't the same");
            }

            if (ModelState.IsValid)
            {
                Dto.User user = registrationViewModel.ToEntity();

                //Save / Register user
                AddResponse response = userRepository.Add(SessionVars.UserId, user);
                user.Id = response.Id;

                TagRepository tagRepository = new TagRepository(Context);
                tagRepository.Add(user.Id, new Tag() { Name = "ExpensesTag" });

                SessionVars.UserId = user.Id;
                SessionVars.UserName = string.Format("{0} {1} {2}", user.FirstName, user.MiddleName, user.LastName).Replace("  ", " "); //The last replacing will work when user don't have middle name. It will correct full name format.
                var ticket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddSeconds(1800), false, "SimpleUser"); //TODO: Implement more rolles if it will be needed for the task, or I could user MemberShip provide. Just wanted to show my experience with authentication.

                var strEncryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strEncryptedTicket);
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(registrationViewModel);
            }
        }

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
                filterContext.HttpContext.Response.Redirect("/Account/LogOn?ReturnUrl=" + 
                    HttpUtility.UrlEncode(filterContext.HttpContext.Request.RawUrl));              
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
