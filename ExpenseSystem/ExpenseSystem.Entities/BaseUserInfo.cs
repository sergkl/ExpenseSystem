using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseSystem.Entities
{
    public class BaseUserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public BaseUserInfo()
        {
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
        }
    }
}
