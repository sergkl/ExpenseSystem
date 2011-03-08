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

        [Dependency]
        public IExpenseRecordRepository ExpenseRecordRepository { get; set; }

        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
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

        [HttpPost]
        public ActionResult GetExpenseRecords(int tagId, bool includeBranchesResuls)
        {
            ExpensesViewModel expensesViewModel = new ExpensesViewModel();
            expensesViewModel.ExpenseRecords = ExpenseRecordRepository.GetExpenseRecordsByTag(SessionVars.UserId, tagId);
            expensesViewModel.TagsFullWay = TagRepository.GetTagFullName(SessionVars.UserId, tagId).Object;
            return PartialView("ExpenseRecordsPartial", expensesViewModel);
        }

        public ActionResult AddExpenseRecord(string description, decimal price, int tagId, DateTime dateStamp)
        {
            Response response = ExpenseRecordRepository.Add(SessionVars.UserId, description, price, tagId, dateStamp);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteExpenseRecord(int expenseRecordId)
        {
            Response response = ExpenseRecordRepository.Delete(SessionVars.UserId, expenseRecordId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditExpenseRecord(int expenseRecordId, string description, decimal price, int tagId, DateTime dateStamp)
        {
            Response response = ExpenseRecordRepository.Edit(SessionVars.UserId, expenseRecordId, description, price, tagId, dateStamp);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
