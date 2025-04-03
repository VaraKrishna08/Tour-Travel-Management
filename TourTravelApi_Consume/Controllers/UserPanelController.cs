using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Consume.Models;
using Newtonsoft.Json;
using TourTravelApi_Consume.Service;
using System.Text;
namespace TourTravelApi_Consume.Controllers
{
    public class UserPanelController : Controller
    {
        private readonly ApiService _apiService;
        private readonly HttpClient _httpClient;

        
        
        public UserPanelController(ApiService apiService, HttpClient httpClient)
        {
            _apiService = apiService;
            _httpClient = httpClient;
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public async Task<IActionResult> Packages()
        {
            var packages = await GetPackageAsync();
            return View(packages);
        }

        private async Task<List<PackageModel>> GetPackageAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("http://localhost:5241/api/Package");
                return JsonConvert.DeserializeObject<List<PackageModel>>(response);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Failed to fetch packages.";
                return new List<PackageModel>();
            }
        }
        private async Task<List<CustomerModel>> GetCustomerAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("http://localhost:5241/api/Customer");
                return JsonConvert.DeserializeObject<List<CustomerModel>>(response);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Failed to fetch packages.";
                return new List<CustomerModel>();
            }
        }
        public IActionResult Customers()
        {
            return View();
        }
        public async Task<IActionResult> Destination()
        {
            var destination = await GetDestinationAsync();
            return View(destination);
        }

        private async Task<List<DestinationModel>> GetDestinationAsync()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5241/api/Destination");
            return JsonConvert.DeserializeObject<List<DestinationModel>>(response);
        }

        public async Task<IActionResult> TourBooking()
        {
            ViewBag.Customers = await GetCustomerAsync(); // Fetch customers dynamically
            ViewBag.Packages = await GetPackageAsync(); // Fetch packages dynamically
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid booking data. Please check your inputs.";
                return RedirectToAction("TourBooking");
            }

            try
            {
                model.BookingDate = DateTime.Now; // Automatically set booking date

                var jsonData = JsonConvert.SerializeObject(model);
                Console.WriteLine("Sending to API: " + jsonData); // Log request data

                var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5241/api/Booking", jsonContent);
                var responseBody = await response.Content.ReadAsStringAsync(); // Get API response

                Console.WriteLine("API Response: " + responseBody); // Log API response

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Booking successful!";
                    return RedirectToAction("TourBooking");
                }
                else
                {
                    TempData["ErrorMessage"] = $"Booking failed. API response: {responseBody}";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
            }

            return RedirectToAction("TourBooking");
        }

        public IActionResult Testinomial()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
