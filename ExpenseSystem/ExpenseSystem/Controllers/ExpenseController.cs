using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseSystem.ViewModels.Home;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.Repositories;
using Microsoft.Practices.Unity;
using ExpenseSystem.Repositories.Interfaces;

namespace ExpenseSystem.Controllers
{
    public class ExpenseController : BaseController
    {
        [Dependency]
        public ITagRepository TagRepository { get; set; }

        [Dependency]
        public IExpenseRecordRepository ExpenseRecordRepository { get; set; }

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
