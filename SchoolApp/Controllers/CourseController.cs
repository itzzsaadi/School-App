using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // READ
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Course.ToListAsync();
            return View(courses);
        }

        // CREATE GET
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            _context.Course.Add(course);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Course successfully add ho gaya!";
            return RedirectToAction("Index");
        }

        // DELETE GET
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // if id is not given popup dikhao
            if (id == 0)            {
                TempData["Error"] = "Course ID missing!";
                return RedirectToAction("Index");
            }
            var course = await _context.Course.FindAsync(id);
            if (course == null)
                return NotFound();
            return View(course);
        }

        // DELETE POST
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id, Course course)
        {
            var existing = await _context.Course.FindAsync(id);
            if (existing == null)
                return NotFound();

            _context.Course.Remove(existing);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Course delete ho gaya — Students unlink ho gaye!";
            return RedirectToAction("Index");
        }
    }
}