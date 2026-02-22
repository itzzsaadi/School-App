using Microsoft.AspNetCore.Mvc;
using SchoolApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // READ - Students ki list
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return Json(students);
        }
        // Add Student
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Json(student);
        }
        // Update Student
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
        {
            var existing = await _context.Students.FindAsync(id);

            if (existing == null)
                return NotFound("Student nahi mila!");

            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Age = student.Age;
            existing.PhoneNumber = student.PhoneNumber;

            await _context.SaveChangesAsync();
            return Json(existing);
        }
    }
}