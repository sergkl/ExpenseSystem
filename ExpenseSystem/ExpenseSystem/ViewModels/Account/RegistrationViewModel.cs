using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSystem.ViewModels.Account
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        [Chars(ErrorMessage = "Only characters are allowed")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        [Chars(ErrorMessage = "Only characters are allowed")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        [Chars(ErrorMessage = "Only characters are allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        [Email(ErrorMessage = "Email address is invalid")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Phone is invalid. Only digits are allowed")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Login is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string ConfirmPassword { get; set; }
    }
}