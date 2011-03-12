using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseSystem.ViewModels.Report
{
    /// <summary>
    /// IndexViewModel view model contains intermediate data between DTO object and view
    /// </summary>
    public class IndexViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalSpentAmount { get; set; }
        public TagResult ParentTagResult { get; set; }
    }
}