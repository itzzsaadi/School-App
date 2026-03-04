//building course model for students
using System.ComponentModel.DataAnnotations;
namespace SchoolApp.Models
{
    public class Course
    {
        //add id for course
        public int Id { get; set; }
        //add name for course
        [Required (ErrorMessage = "Name zaroori hai!")]
        public string Name { get; set; }
        //add credit hours for course
        [Required(ErrorMessage = "Credit Hours zaroori hain!")]
        [Range(1, 6, ErrorMessage = "Credit Hours 1 aur 6 ke beech hone chahiye!")]
        public int CreditHours { get; set; }
        // One to Many — Ek course mein kai students
        public List<Student> Students { get; set; } = new List<Student>();
    }
}