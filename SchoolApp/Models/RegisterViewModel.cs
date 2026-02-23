using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email zaroori hai!")]
        [EmailAddress(ErrorMessage = "Valid email address do!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password zaroori hai!")]
        [MinLength(6, ErrorMessage = "Password kam se kam 6 characters ka hona chahiye!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must containan uppercase letter, number,and special character!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password zaroori hai!")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}