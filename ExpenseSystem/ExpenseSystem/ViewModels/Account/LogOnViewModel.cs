using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSystem.Models
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        [Chars(ErrorMessage = "Only characters are allowed")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        [Chars(ErrorMessage = "Only characters are allowed")]
        public string Password { get; set; }
    }
}