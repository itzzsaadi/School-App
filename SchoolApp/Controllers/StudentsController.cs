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
            // Session mein visit count store karo
            int visitCount = HttpContext.Session.GetInt32("VisitCount") ?? 0;
            visitCount++;
            HttpContext.Session.SetInt32("VisitCount", visitCount);
            ViewBag.VisitCount = visitCount;

            _logger.LogInformation("Students list dekhi gayi — {Time}", DateTime.Now);

            var students = await _context.Students.
            Include(s => s.Course).
            ToListAsync();
            return View(students);
        }
        [Authorize(Roles = "Admin")]
        // GET - Form dikhao
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Course list dropdown ke liye
            ViewBag.Courses = await _context.Course.ToListAsync();
            return View();
        }
        [Authorize(Roles = "Admin")]
        // Add Student
        [HttpPost]
        public async Task<IActionResult> Create(Student student, IFormFile? photo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = await _context.Course.ToListAsync();
                return View(student);
            }
            if (photo != null && photo.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                // Folder nahi hai toh banao
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // Unique file name banao
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                student.PhotoPath = "/uploads/" + fileName;
            }
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
            var student = await _context.Students.
            Include(s => s.Course).
            FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                TempData["Error"] = "TRUE";
                return RedirectToAction("Index");
            }
            ViewBag.Courses = await _context.Course.ToListAsync();
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // POST - Update save karo
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            var existing = await _context.Students.
            Include(s => s.Course).
            FirstOrDefaultAsync(s => s.Id == id);

            if (existing == null)
            {
                TempData["Error"] = "TRUE";
                return RedirectToAction("Index");
            }
            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Age = student.Age;
            existing.PhoneNumber = student.PhoneNumber;
            existing.CourseId = student.CourseId;

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