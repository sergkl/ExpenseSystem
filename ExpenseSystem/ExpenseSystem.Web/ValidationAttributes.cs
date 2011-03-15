using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSystem
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() : base(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$") { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CharsAttribute : RegularExpressionAttribute
    {
        public CharsAttribute() : base(@"^[A-Za-z]+$") { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PhoneAttribute : RegularExpressionAttribute
    {
        public PhoneAttribute() : base(@"^[0-9]+$") { }
    }
}