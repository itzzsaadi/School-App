using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Name zaroori hai!")]
        public string Name { get; set; }
        [Required (ErrorMessage = "Email zaroori hai!")]
        [EmailAddress(ErrorMessage = "Valid email address do!")]
        public string Email { get; set; }
        [Required (ErrorMessage = "Age zaroori hai!")]
        [Range(1, 120, ErrorMessage = "Age 1 se 120 ke beech honi chahiye!")]
        public int Age { get; set; }
        [Required (ErrorMessage = "Phone number zaroori hai!")]
        [Phone(ErrorMessage = "Valid phone number do!")]
        public string PhoneNumber { get; set; }
    }
}