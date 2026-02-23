using System.ComponentModel.DataAnnotations;
namespace SchoolApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email zaroori hai!")]
        [EmailAddress(ErrorMessage = "Valid email address do!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password zaroori hai!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}