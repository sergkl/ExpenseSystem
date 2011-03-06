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
        public ExpenseSystemEntities Context { get; set; }
        public SessionHelper SessionVars { get; set; }

        public BaseController()
        {
            Context = new ExpenseSystemEntities();
            SessionVars = new SessionHelper();
            ViewData["UserName"] = SessionVars.UserName;
        }
    }
}