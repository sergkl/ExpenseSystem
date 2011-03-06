using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSystem
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() : base(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$") { }
    }

    public class CharsAttribute : RegularExpressionAttribute
    {
        public CharsAttribute() : base(@"^[A-Za-z]+$") { }
    }

    public class PhoneAttribute : RegularExpressionAttribute
    {
        public PhoneAttribute() : base(@"^[0-9]+$") { }
    }
}