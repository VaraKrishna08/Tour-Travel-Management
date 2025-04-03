//using System.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using TourTravelApi_Consume.Filters;
//using TourTravelApi_Consume.Models;

//namespace TourTravelApi_Consume.Controllers
//{
//    [CheckAccess]
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Consume.Filters;
using TourTravelApi_Consume.Models;

namespace TourTravelApi_Consume.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Home/Index
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
    }
}
//// POST: Home/Index
//[HttpPost]
//public IActionResult Index(string email, string password)
//{

//    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
//    {
//        ViewBag.Error = "Email and password are required.";
//        return View(); // Return to the same view if validation fails
//    }
//     if (email == "admin@admin.com" && password == "password123") // Example login
//    {

//        return RedirectToAction("Dashboard", "Home");
//    }
//    else
//    {
//        ViewBag.Error = "Invalid email or password.";
//        return View(); 
//    }
//}