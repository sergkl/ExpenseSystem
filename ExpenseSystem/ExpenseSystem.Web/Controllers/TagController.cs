using System.Web.Mvc;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories.Responses;
using Microsoft.Practices.Unity;
using ExpenseSystem.Entities;

namespace ExpenseSystem.Controllers
{
    /// <summary>
    /// Controller helps to work with tags
    /// </summary>
    [UserAuthorize]
    public class TagController : BaseController
    {
        /// <summary>
        /// Tag repository
        /// </summary>
        [Dependency]
        public ITagRepository TagRepository { get; set; }

        /// <summary>
        /// Action gets tag with children and tag for all levels in the tree at all using user identifier
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTagsTree()
        {
            if (SessionVars.UserId != 0)
            {
                GetObjectResponse<Tag> response = TagRepository.GetParentTagByUserId(SessionVars.UserId);
                if (!response.IsError)
                    return View("~/Views/Home/Index.cshtml", response.Object);
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Action deletes tag from the system
        /// </summary>
        /// <param name="tagId">Tag identifier</param>
        /// <returns>Execution result as json structure</returns>
        [HttpPost]
        [PreventCSRF]
        public ActionResult DeleteTag(int tagId)
        {
            Response response = TagRepository.DeleteById(SessionVars.UserId, tagId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method adds tag to database.
        /// </summary>
        /// <param name="name">Name for new tag</param>
        /// <param name="parentId">Parent identifier</param>
        /// <returns>Execution result as json structure</returns>
        [HttpPost]
        [PreventCSRF]
        public ActionResult AddTag(string name, int? parentId)
        {
            if (parentId == null)
                parentId = TagRepository.GetParentTagByUserId(SessionVars.UserId).Object.Id;
            AddResponse response = TagRepository.Add(SessionVars.UserId, name, (int)parentId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method changes tag name
        /// </summary>
        /// <param name="tagId">Tag identifier</param>
        /// <param name="tagName">New tag name</param>
        /// <returns>Execution result as json structure</returns>
        [HttpPost]
        [PreventCSRF]
        public ActionResult ChangeTagName(int tagId, string tagName)
        {
            Response response = TagRepository.ChangeTagName(SessionVars.UserId, tagId, tagName);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
