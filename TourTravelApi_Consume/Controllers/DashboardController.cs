using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TourTravelApi_Consume.Service;
using TourTravelApi_Consume.Models;

namespace TourTravelApi_Consume.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApiService _apiService;

        public DashboardController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> DashboardUi()
        {
            //// Check if the user is logged in
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
            //{
            //    return RedirectToAction("Signup", "Customer");
            //}

            // Fetch data using the API service
            var customers = await _apiService.GetCustomersAsync();
            var bookings = await _apiService.GetBookingsAsync();
            var feedbacks = await _apiService.GetFeedbackAsync();
            var destinations = await _apiService.GetDestinationAsync(); // Fetch destinations

            // Store data in ViewBag
            ViewBag.TotalCustomers = customers.Count;
            ViewBag.TotalBookings = bookings.Count;
            ViewBag.TotalFeedbacks = feedbacks.Count;
            ViewBag.TotalDestinations = destinations.Count; // Destination count
            ViewBag.RecentBookings = bookings.Take(5).ToList(); // Top 5 bookings
            ViewBag.feedbacks = feedbacks.Take(5).ToList(); // Top 5 bookings
            ViewBag.Destinations = destinations; // Pass destinations

            return View();
        }
    }
}

////}using Microsoft.AspNetCore.Mvc;
//using TourTravelApi_Consume.Service;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using TourTravelApi_Consume.Models;

//namespace TourTravelApi_Consume.Controllers
//{
//    public class DashboardController : Controller
//    {
//        private readonly ApiService _apiService;

//        public DashboardController(ApiService apiService)
//        {
//            _apiService = apiService;
//        }

//        public async Task<IActionResult> DashboardUi()
//        {
//            //// Check if the user is logged in
//            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("JwtToken")))
//            //{
//            //    return RedirectToAction("Signup", "Customer");
//            //}

//            var customers = await _apiService.GetCustomersAsync();
//            var bookings = await _apiService.GetBookingsAsync();
//            var feedbacks = await _apiService.GetFeedbackAsync();
//            //var packages = await _apiService.GetPackageAsync();
//            ViewBag.TotalCustomers = customers.Count;
//            ViewBag.TotalBookings = bookings.Count;
//            ViewBag.TotalFeedbacks = feedbacks.Count;

//            return View(bookings); // Pass the recent bookings
//        }

//    }
//}
