using System.ComponentModel.DataAnnotations;

namespace ExpenseSystem.Models
{
    /// <summary>
    /// LogOn view model contains intermediate data between DTO object and view
    /// </summary>
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string Password { get; set; }
    }
}