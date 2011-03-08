using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseSystem.ViewModels.Home;
using ExpenseSystem.Repositories;
using ExpenseSystem.Entities;
using ExpenseSystem.Repositories.Responses;
using Microsoft.Practices.Unity;
using ExpenseSystem.Repositories.Interfaces;

namespace ExpenseSystem.Controllers
{
    [UserAuthorize]
    public class HomeController : BaseController
    {
        [Dependency]
        public ITagRepository TagRepository { get; set; }

        public ActionResult Index()
        {
            if (SessionVars.UserId != 0)
            {
                GetObjectResponse<Tag> response = TagRepository.GetParentTagByUserId(SessionVars.UserId);
                if (!response.IsError)
                    return View(response.Object);
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
    }
}
