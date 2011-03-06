using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseSystem.ViewModels.Home;
using ExpenseSystem.Repositories;
using ExpenseSystem.Entities;
using ExpenseSystem.Repositories.Responses;

namespace ExpenseSystem.Controllers
{
    [UserAuthorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            TagRepository tagRepository = new TagRepository(Context);
            if (SessionVars.UserId != 0)
            {
                GetObjectResponse<Tag> response = tagRepository.GetParentTagByUserId(SessionVars.UserId);
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

        public ActionResult DeleteTag(int tagId)
        {
            TagRepository tagRepository = new TagRepository(Context);
            Response response = tagRepository.DeleteById(SessionVars.UserId, tagId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddTag(string name, int? parentId)
        {
            TagRepository tagRepository = new TagRepository(Context);
            if (parentId == null)
                parentId = tagRepository.GetParentTagByUserId(SessionVars.UserId).Object.Id;
            AddResponse response = tagRepository.Add(SessionVars.UserId, name, (int)parentId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeTagName(int tagId, string tagName)
        {
            TagRepository tagRepository = new TagRepository(Context);
            Response response = tagRepository.ChangeTagName(SessionVars.UserId, tagId, tagName);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetExpenseRecords(int tagId, bool includeBranchesResuls)
        {
            ExpensesViewModel expensesViewModel = new ExpensesViewModel();
            expensesViewModel.ExpenseRecords = new ExpenseRecordRepository(Context).GetExpenseRecordsByTag(SessionVars.UserId, tagId);
            expensesViewModel.TagsFullWay = new TagRepository(Context).GetTagFullName(SessionVars.UserId, tagId).Object;
            return PartialView("ExpenseRecordsPartial", expensesViewModel);
        }

        public ActionResult AddExpenseRecord(string description, decimal price, int tagId, DateTime dateStamp)
        {
            ExpenseRecordRepository expenseRecordRepository = new ExpenseRecordRepository(Context);
            Response response = expenseRecordRepository.Add(SessionVars.UserId, description, price, tagId, dateStamp);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteExpenseRecord(int expenseRecordId)
        {
            ExpenseRecordRepository expenseRecordRepository = new ExpenseRecordRepository(Context);
            Response response = expenseRecordRepository.Delete(SessionVars.UserId, expenseRecordId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditExpenseRecord(int expenseRecordId, string description, decimal price, int tagId, DateTime dateStamp)
        {
            ExpenseRecordRepository expenseRecordRepository = new ExpenseRecordRepository(Context);
            Response response = expenseRecordRepository.Edit(SessionVars.UserId, expenseRecordId, description, price, tagId, dateStamp);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
