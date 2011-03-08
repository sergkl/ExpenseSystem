using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Controllers
{
    public class TagController : BaseController
    {
        [Dependency]
        public ITagRepository TagRepository { get; set; }

        public ActionResult DeleteTag(int tagId)
        {
            Response response = TagRepository.DeleteById(SessionVars.UserId, tagId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddTag(string name, int? parentId)
        {
            if (parentId == null)
                parentId = TagRepository.GetParentTagByUserId(SessionVars.UserId).Object.Id;
            AddResponse response = TagRepository.Add(SessionVars.UserId, name, (int)parentId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeTagName(int tagId, string tagName)
        {
            Response response = TagRepository.ChangeTagName(SessionVars.UserId, tagId, tagName);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
