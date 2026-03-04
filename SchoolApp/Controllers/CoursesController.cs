using Microsoft.AspNetCore.Mvc;
using SchoolApp.Models;
namespace SchoolApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: Courses - Sab courses dikhao
        public IActionResult Index()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }
        // GET: Courses/Create - Course banane ka form dikhao
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // POST: Courses/Create - Course banane ka form submit karo
        [HttpPost]
        public IActionResult Create(Course model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Courses.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Course ban gaya!";
            return RedirectToAction("Index");
        }
    }
}