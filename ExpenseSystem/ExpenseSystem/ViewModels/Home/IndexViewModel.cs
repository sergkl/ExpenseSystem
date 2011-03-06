using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseSystem.Entities;

namespace ExpenseSystem.ViewModels.Home
{
    public class IndexViewModel
    {
        public string FullGroupName { get; set; }
        public Tag TagsTree { get; set; }
        public List<ExpenseRecord> ExpenseRecords { get; set; }
    }
}