using System;

namespace ExpenseSystem.ViewModels.Report
{
    /// <summary>
    /// IndexViewModel view model contains intermediate data between DTO object and view
    /// </summary>
    public class IndexViewModel
    {
        public string SessionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalSpentAmount { get; set; }
        public TagResult ParentTagResult { get; set; }
    }
}