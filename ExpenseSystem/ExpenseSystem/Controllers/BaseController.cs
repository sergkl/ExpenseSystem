using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseSystem.Model;
using ExpenseSystem.Models;
using ExpenseSystem.Helpers;

namespace ExpenseSystem.Controllers
{
    public abstract class BaseController : Controller
    {
        public SessionHelper SessionVars { get; set; }

        public BaseController()
        {
            SessionVars = new SessionHelper();
            ViewData["UserName"] = SessionVars.UserName;
        }
    }
}