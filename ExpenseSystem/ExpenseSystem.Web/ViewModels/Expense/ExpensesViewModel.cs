using System.Collections.Generic;
using ExpenseSystem.Entities;

namespace ExpenseSystem.ViewModels.Expense
{
    /// <summary>
    /// ExpensesViewModel view model contains intermediate data between DTO object and view
    /// </summary>
    public class ExpensesViewModel
    {
        public string TagsFullWay { get; set; }
        public List<ExpenseRecord> ExpenseRecords { get; set; }

        public ExpensesViewModel()
        {
            ExpenseRecords = new List<ExpenseRecord>();
            TagsFullWay = string.Empty;
        }
    }
}