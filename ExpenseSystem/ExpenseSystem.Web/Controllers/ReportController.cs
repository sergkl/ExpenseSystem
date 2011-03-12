using System;
using System.Web.Mvc;
using ExpenseSystem.Entities;
using ExpenseSystem.Repositories;
using ExpenseSystem.ViewModels.Report;
using Microsoft.Practices.Unity;

namespace ExpenseSystem.Controllers
{
    /// <summary>
    /// Controller creates the report to show how much costs were spent by tags and by date range
    /// </summary>
    [UserAuthorize]
    public class ReportController : BaseController
    {
        /// <summary>
        /// The date is used as start date for the report
        /// </summary>
        DateTime startDate;

        /// <summary>
        /// The date is used as finish date for the report
        /// </summary>
        DateTime endDate;

        /// <summary>
        /// Repository for tags data
        /// </summary>
        [Dependency]
        public TagRepository TagRepository { get; set; }

        /// <summary>
        /// Action sets initial values for date range.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.StartDate = DateTime.Now.AddMonths(-1).Date;
            indexViewModel.EndDate = DateTime.Now.Date;
            return View(indexViewModel);
        }

        /// <summary>
        /// Action forms the report
        /// </summary>
        /// <param name="indexViewModel">The model which contains start and end date</param>
        /// <returns>Tree of tags with statistical information</returns>
        [HttpPost]
        public ActionResult Index(IndexViewModel indexViewModel)
        {
            if (indexViewModel.StartDate > indexViewModel.EndDate)
            {
                ModelState.AddModelError("StartDate", "End date must be greater than start date");
            }

            if (ModelState.IsValid)
            {
                startDate = indexViewModel.StartDate;
                endDate = indexViewModel.EndDate;
                Tag tag = TagRepository.GetParentTagByUserId(SessionVars.UserId).Object;
                TagResult tagResult = new TagResult();
                tagResult.TagId = tag.Id;
                indexViewModel.ParentTagResult = GetTagResult(tagResult);
                CalculatePercents(indexViewModel.ParentTagResult, indexViewModel.ParentTagResult.SpentAmount);
                return View(indexViewModel);
            }
            else
            {
                return View(indexViewModel);
            }
        }

        /// <summary>
        /// Calculates report by tag but without percentage values.
        /// </summary>
        /// <param name="tagResult">Tag which is used to calculate the result</param>
        /// <returns>Tag with results for report, without percentage values.</returns>
        private TagResult GetTagResult(TagResult tagResult)
        {
            Tag tag = TagRepository.GetById(SessionVars.UserId, tagResult.TagId).Object;
            tagResult.SpentAmount = TagRepository.GetSpentAmountByTag(SessionVars.UserId, tagResult.TagId, startDate, endDate).Object;
            tagResult.TagName = tag.Name;
            foreach (Tag childTag in tag.Children)
            {
                TagResult childTagResult = new TagResult();
                childTagResult.TagId = childTag.Id;
                tagResult.Children.Add(GetTagResult(childTagResult));
            }
            return tagResult;
        }

        /// <summary>
        /// Method calculated percentage values for all level in the tree of tags
        /// </summary>
        /// <param name="tagResult">Tree of tags</param>
        /// <param name="totalAmount">Total amount. So it is the value which we use to calculate percentage values. i.e. [Branch Result] / [Total amount]</param>
        /// <returns></returns>
        private TagResult CalculatePercents(TagResult tagResult, decimal totalAmount)
        {
            tagResult.Percentage = Math.Round(Convert.ToDouble(tagResult.SpentAmount / totalAmount) * 100.0, 2);
            foreach (TagResult child in tagResult.Children)
            {
                CalculatePercents(child, totalAmount);
            }
            return tagResult;
        }
    }
}
