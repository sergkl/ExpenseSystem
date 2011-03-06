using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseSystem.Entities;

namespace ExpenseSystem.ViewModels.Home
{
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