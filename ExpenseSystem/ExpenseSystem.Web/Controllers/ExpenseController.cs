using System;
using System.Web.Mvc;
using ExpenseSystem.Repositories;
using ExpenseSystem.Repositories.Interfaces;
using ExpenseSystem.Repositories.Responses;
using ExpenseSystem.ViewModels.Home;
using Microsoft.Practices.Unity;

namespace ExpenseSystem.Controllers
{
    /// <summary>
    /// Controller helps working with expense records
    /// </summary>
    [UserAuthorize]
    public class ExpenseController : BaseController
    {
        /// <summary>
        /// Tag repository
        /// </summary>
        [Dependency]
        public ITagRepository TagRepository { get; set; }

        /// <summary>
        /// Expense record repository
        /// </summary>
        [Dependency]
        public IExpenseRecordRepository ExpenseRecordRepository { get; set; }

        /// <summary>
        /// Action gets expense records
        /// </summary>
        /// <param name="tagId">Group tag. What data will search for.</param>
        /// <param name="includeBranchesResuls">Flag which shows should we include sub branches results or no</param>
        /// <returns>The partial view which contains tag names with all levels and list of expense records</returns>
        [HttpGet]
        [PreventCSRF]
        public ActionResult GetExpenseRecords(int tagId, bool includeBranchesResuls)
        {
            ExpensesViewModel expensesViewModel = new ExpensesViewModel();
            expensesViewModel.ExpenseRecords = ExpenseRecordRepository.GetExpenseRecordsByTag(SessionVars.UserId, tagId).Object;
            expensesViewModel.TagsFullWay = TagRepository.GetTagFullName(SessionVars.UserId, tagId).Object;
            return PartialView("ExpenseRecordsPartial", expensesViewModel);
        }

        /// <summary>
        /// Action adds expense record to the system
        /// </summary>
        /// <example>
        /// <code>
        /// AddExpenseRecord("Parties", 12, 1, DateTime.Now.Date);
        /// </code>
        /// </example>
        /// <param name="description">Description for expense record</param>
        /// <param name="price">Price</param>
        /// <param name="tagId">Tag identifier</param>
        /// <param name="dateStamp">Date stamp</param>
        /// <returns>Execution result as json data</returns>
        [HttpPost]
        [PreventCSRF]
        public ActionResult AddExpenseRecord(string description, decimal price, int tagId, DateTime? dateStamp)
        {
            Response response = ExpenseRecordRepository.Add(SessionVars.UserId, description, price, tagId, dateStamp);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete expense record
        /// </summary>
        /// <param name="expenseRecordId">Expense record identifier</param>
        /// <returns>Execution result</returns>
        [HttpPost]
        [PreventCSRF]
        public ActionResult DeleteExpenseRecord(int expenseRecordId)
        {
            Response response = ExpenseRecordRepository.Delete(SessionVars.UserId, expenseRecordId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Action edits an expense record. Expense record will be searched by expense record identifier
        /// This method can change description, pricde, tag identifier (so we can change branches for expense record), and date for expense record.
        /// </summary>
        /// <param name="expenseRecordId">Expense record identifier</param>
        /// <param name="description">Description</param>
        /// <param name="price">Price</param>
        /// <param name="tagId">Tag identifier</param>
        /// <param name="dateStamp">Date stamp. This data is used in forming reports</param>
        /// <returns>Execution result as json structure</returns>
        [HttpPost]
        [PreventCSRF]
        public ActionResult EditExpenseRecord(int expenseRecordId, string description, decimal price, int tagId, DateTime? dateStamp)
        {
            Response response = ExpenseRecordRepository.Edit(SessionVars.UserId, expenseRecordId, description, price, tagId, dateStamp);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
