using Microsoft.AspNetCore.Mvc;
using SchoolApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SchoolApp.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(AppDbContext context, ILogger<StudentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // READ - Students ki list
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Students list dekhi gayi — {Time}", DateTime.Now);
            var students = await _context.Students.ToListAsync();
            return View(students);
        }
        [Authorize(Roles = "Admin")]
        // GET - Form dikhao
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        // Add Student
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Naya student add kiya — {Name}", student.Name);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        // GET - Edit form dikhao
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                TempData["Error"] = "TRUE";
                return RedirectToAction("Index");
            }
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // POST - Update save karo
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            var existing = await _context.Students.FindAsync(id);

            if (existing == null)
            {
                TempData["Error"] = "TRUE";
                return RedirectToAction("Index");
            }
            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Age = student.Age;
            existing.PhoneNumber = student.PhoneNumber;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Student update kiya gaya — {Name}", existing.Name);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        // GET - Confirmation page dikhao
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                TempData["Error"] = "TRUE";
                return RedirectToAction("Index");
            }

            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // POST - Actually delete karo
        [HttpPost]
        public async Task<IActionResult> Delete(int id, Student student)
        {
            var existing = await _context.Students.FindAsync(id);

            if (existing == null)
            {
                TempData["Error"] = "TRUE";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Delete"] = "Deleted";
            }

            _context.Students.Remove(existing);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Student delete kiya gaya — {Name}", existing.Name);
            return RedirectToAction("Index");
        }
    }
}