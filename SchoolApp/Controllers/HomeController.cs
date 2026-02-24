using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SchoolApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    // GET - Error page for status codes like 404,500 etc.
    [AllowAnonymous]
    public IActionResult Error(int? statusCode = null)
    {
        if (statusCode == 404)
            ViewBag.ErrorMessage = "Yeh page exist nahi karta!";
        else if (statusCode == 500)
            ViewBag.ErrorMessage = "Server mein kuch masla ho gaya!";
        else
            ViewBag.ErrorMessage = "Kuch galat ho gaya!";

        return View();
    }
}
