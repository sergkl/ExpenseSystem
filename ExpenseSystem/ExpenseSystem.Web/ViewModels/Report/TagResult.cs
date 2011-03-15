using System.Collections.Generic;

namespace ExpenseSystem.ViewModels.Report
{
    /// <summary>
    /// TagResult view model contains intermediate data between DTO object and view
    /// </summary>
    public class TagResult
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public decimal SpentAmount { get; set; }
        public double Percentage { get; set; }
        public List<TagResult> Children { get; set; }

        public TagResult()
        {
            TagName = string.Empty;
            Children = new List<TagResult>();
        }
    }
}