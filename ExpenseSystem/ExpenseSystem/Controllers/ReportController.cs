using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpenseSystem.ViewModels.Report;
using ExpenseSystem.Repositories;
using Dto = ExpenseSystem.Entities;
using ExpenseSystem.Entities;
using Microsoft.Practices.Unity;

namespace ExpenseSystem.Controllers
{
    public class ReportController : BaseController
    {
        DateTime startDate;
        DateTime endDate;

        [Dependency]
        public TagRepository TagRepository { get; set; }

        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.StartDate = DateTime.Now.AddMonths(-1).Date;
            indexViewModel.EndDate = DateTime.Now.Date;
            return View(indexViewModel);
        }

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
