using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Consume.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.Xml;

namespace TourTravelApi_Consume.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;

            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"]);

            // Set Authorization header with JWT token
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        #region Customer List 
        public async Task<IActionResult> CustomerList()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Customer");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<CustomerModel> customers = new List<CustomerModel>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/customer");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    customers = JsonConvert.DeserializeObject<List<CustomerModel>>(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }

            return View(customers);
        }
        #endregion

        #region Customer 
        public async Task<IActionResult> CustomerAddEdit(int? CustomerID)
        {
            if (CustomerID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/customer/{CustomerID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<CustomerModel>(data);
                    return View(customer);
                }
            }
            return View(new CustomerModel());
        }
        #endregion

        #region Save Customer
        [HttpPost]

        public async Task<IActionResult> Save(CustomerModel customerModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CustomerAddEdit", customerModel);
            }

            // 🚨 Fix: Convert null CustomerID to 0
            customerModel.CustomerID ??= 0; // If null, set to 0

            // Debugging: Check what ID is being passed
            Console.WriteLine($"CustomerID received in Save method: {customerModel.CustomerID}");

            var json = JsonConvert.SerializeObject(customerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;

            try
            {
                if (customerModel.CustomerID == 0) // New Record
                {
                    Console.WriteLine("Performing INSERT operation...");
                    response = await _httpClient.PostAsync("api/customer", content);
                }
                else // Update existing
                {
                    Console.WriteLine("Performing UPDATE operation...");
                    response = await _httpClient.PutAsync($"api/customer/{customerModel.CustomerID}", content);
                }

                // Read API response
                string responseMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {responseMessage}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = customerModel.CustomerID == 0
                        ? "Customer successfully added."
                        : "Customer successfully updated.";
                }
                else
                {
                    TempData["ErrorMessage"] = $"Operation failed: {responseMessage}";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Exception: {ex.Message}";
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return RedirectToAction("CustomerList");
        }
        #endregion
       
        #region Delete Customer
        public async Task<IActionResult> Delete(int CustomerID)
        {
            var response = await _httpClient.DeleteAsync($"api/customer/{CustomerID}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Customer deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the Customer.";
            }
            return RedirectToAction("CustomerList");
        }
        #endregion
        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Please enter both email and password.";
                return View();
            }

            var loginModel = new { Email = email, Password = password };
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5241/api/customer/Logincustomer", content);

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();

                // Store JWT Token in Cookie for Persistent Login
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true, // Prevents JavaScript access (security)
                    Secure = true, // Use HTTPS
                    Expires = rememberMe ? System.DateTime.UtcNow.AddDays(30) : System.DateTime.UtcNow.AddHours(1)
                };
                Response.Cookies.Append("JWTToken", token, cookieOptions);

                // Redirect Based on User Role
                if (email == "kr@gmail.com" && password == "kr123")
                {
                    return RedirectToAction("DashboardUI", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Home", "UserPanel");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid email or password. Please try again.";
                return View();
            }
        }
        #endregion

        #region Signup
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(CustomerModel customerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(customerModel);
            }

            var json = JsonConvert.SerializeObject(customerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5241/api/customer/SignUpcustomer", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Signup successful! Please log in.";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["ErrorMessage"] = "Signup failed. Please try again.";
                return View(customerModel);
            }
        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {

            HttpContext.Session.Remove("JwToken");
            return RedirectToAction("Login", "Customer");
        }
        #endregion
    }
}
//#region Customer Login
//public async Task<IActionResult> Login(LoginModel loginModel)
//{
//    if (!ModelState.IsValid)
//    {
//        return View("Login", loginModel);
//    }

//    var json = JsonConvert.SerializeObject(loginModel);
//    var content = new StringContent(json, Encoding.UTF8, "application/json");

//    HttpResponseMessage response = await _httpClient.PostAsync("api/Customer/Logincustomer", content);

//    if (response.IsSuccessStatusCode)
//    {
//        string data = await response.Content.ReadAsStringAsync();
//        var user = JsonConvert.DeserializeObject<CustomerModel>(data);

//        HttpContext.Session.SetString("CustomerID", user.CustomerID.ToString());
//        HttpContext.Session.SetString("CustomerName", user.FullName);

//        return RedirectToAction("CustomerList");
//    }
//    else
//    {
//        TempData["ErrorMessage"] = "Invalid email or password.";
//        return View("Login");
//    }
//}
//#endregion
//#region Customer Signup
//[HttpPost]
//public async Task<IActionResult> Signup(CustomerModel customerModel)
//{
//    if (!ModelState.IsValid)
//    {
//        return View("Signup", customerModel);
//    }

//    var json = JsonConvert.SerializeObject(customerModel);
//    var content = new StringContent(json, Encoding.UTF8, "application/json");

//    HttpResponseMessage response = await _httpClient.PostAsync("api/Customer/SignUpcustomer", content);

//    if (response.IsSuccessStatusCode)
//    {
//        TempData["SuccessMessage"] = "Signup successful. Please login.";
//        return RedirectToAction("Login");
//    }
//    else
//    {
//        TempData["ErrorMessage"] = "Signup failed. Try again.";
//        return View("Signup");
//    }
//}
//#endregion


//#region Logout
//public IActionResult Logout()
//{
//    HttpContext.Session.Clear();
//    return RedirectToAction("Login", "Customer");
//}
//#endregion


//using Microsoft.AspNetCore.Mvc;
//using TourTravelApi_Consume.Models;
//using System.Text;
//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Diagnostics.Metrics;
//using Microsoft.AspNetCore.Http;
//using System.Threading.Tasks;

//namespace TourTravelApi_Consume.Controllers
//{
//    public class CustomerController : Controller
//    {
//        private readonly IConfiguration _configuration;
//        private readonly HttpClient _httpClient;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IHttpClientFactory _httpClientFactory;

//        public CustomerController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }
//        public CustomerController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
//        {
//            _configuration = configuration;
//            _httpContextAccessor = httpContextAccessor;
//            _httpClient = new HttpClient
//            {
//                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
//            };

//            // Set Authorization header with JWT token
//            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
//            if (!string.IsNullOrEmpty(token))
//            {
//                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
//            }
//        }

//        #region Customer List GetAll
//        public async Task<IActionResult> CustomerList()
//        {
//            var token = HttpContext.Session.GetString("JwtToken");
//            if (string.IsNullOrEmpty(token))
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

//            List<CustomerModel> customers = new List<CustomerModel>();
//            try
//            {
//                HttpResponseMessage response = await _httpClient.GetAsync("api/customer");
//                if (response.IsSuccessStatusCode)
//                {
//                    string data = await response.Content.ReadAsStringAsync();
//                    customers = JsonConvert.DeserializeObject<List<CustomerModel>>(data);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Exception occurred: {ex.Message}");
//            }

//            return View(customers);
//        }

//        //public async Task<IActionResult> CustomerList()
//        //{
//        //    List<CustomerModel> customers = new List<CustomerModel>();

//        //    try
//        //    {
//        //        HttpResponseMessage response = await _httpClient.GetAsync("api/customer");

//        //        if (response.IsSuccessStatusCode)
//        //        {
//        //            string data = await response.Content.ReadAsStringAsync();
//        //            customers = JsonConvert.DeserializeObject<List<CustomerModel>>(data);
//        //        }
//        //        else
//        //        {
//        //            Console.WriteLine($"API Error: {response.StatusCode}");
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine($"Exception occurred: {ex.Message}");
//        //    }

//        //    return View(customers);
//        //}
//        #endregion

//        #region Customer Add Edit 
//        public async Task<IActionResult> CustomerAddEdit(int? CustomerID)
//        {
//            if (CustomerID.HasValue)
//            {
//                var response = await _httpClient.GetAsync($"api/customer/{CustomerID}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var data = await response.Content.ReadAsStringAsync();
//                    var customer = JsonConvert.DeserializeObject<CustomerModel>(data);
//                    return View(customer);
//                }
//            }
//            return View(new CustomerModel());
//        }
//        #endregion

//        #region Customer Save
//        [HttpPost]
//        public async Task<IActionResult> Save(CustomerModel customerModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View("CustomerAddEdit", customerModel);
//            }

//            var json = JsonConvert.SerializeObject(customerModel);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");
//            HttpResponseMessage response;

//            if (customerModel.CustomerID == null)
//                response = await _httpClient.PostAsync("api/Customer", content);
//            else
//                response = await _httpClient.PutAsync($"api/Customer/{customerModel.CustomerID}", content);

//            if (response.IsSuccessStatusCode)
//            {
//                TempData["SuccessMessage"] = customerModel.CustomerID == null
//                    ? "Customer successfully added."
//                    : "Customer successfully updated.";
//            }
//            else
//            {
//                TempData["SuccessMessage"] = "Operation failed. Please try again.";
//            }

//            return RedirectToAction("CustomerList");
//        }
//        #endregion

//        #region Customer Delete
//        public async Task<IActionResult> Delete(int CustomerID)
//        {
//            var response = await _httpClient.DeleteAsync($"api/customer/{CustomerID}");
//            if (response.IsSuccessStatusCode)
//            {
//                TempData["DeleteSuccessMessage"] = "Customer deleted successfully.";
//            }
//            else
//            {
//                TempData["DeleteSuccessMessage"] = "Failed to delete the Customer.";
//            }
//            return RedirectToAction("CustomerList");
//        }
//        #endregion

//        // Signup
//        [HttpPost]
//        public async Task<IActionResult> Signup(CustomerModel customer)
//        {
//            using (var client = _httpClientFactory.CreateClient())
//            {
//                var json = JsonConvert.SerializeObject(customer);
//                var content = new StringContent(json, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await client.PostAsync("https://your-api-url/api/customer/signup", content);

//                if (response.IsSuccessStatusCode)
//                {
//                    return RedirectToAction("Login");
//                }
//                else
//                {
//                    ViewBag.Error = "Signup failed. Please try again.";
//                    return View();
//                }
//            }
//        }

//        // Login
//        [HttpPost]
//        public async Task<IActionResult> Login(LoginModel loginModel)
//        {
//            using (var client = _httpClientFactory.CreateClient())
//            {
//                var json = JsonConvert.SerializeObject(loginModel);
//                var content = new StringContent(json, Encoding.UTF8, "application/json");

//                HttpResponseMessage response = await client.PostAsync("https://your-api-url/api/customer/login", content);

//                if (response.IsSuccessStatusCode)
//                {
//                    var tokenResponse = await response.Content.ReadAsStringAsync();
//                    var result = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse);

//                    HttpContext.Session.SetString("CustomerID", result.CustomerID.ToString());
//                    HttpContext.Session.SetString("FullName", result.FullName);
//                    HttpContext.Session.SetString("Email", result.Email);
//                    HttpContext.Session.SetString("AuthToken", result.Token);

//                    return RedirectToAction("Dashboard", "Home");
//                }
//                else
//                {
//                    ViewBag.Error = "Invalid email or password.";
//                    return View();
//                }
//            }
//        }

//        // Logout
//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();
//            return RedirectToAction("Login");
//        }
//    }
//}


//#region Customer Signup
//[HttpPost]
//public async Task<IActionResult> Signup(CustomerModel customerModel)
//{
//    if (!ModelState.IsValid)
//    {
//        return View("Signup", customerModel);
//    }

//    var json = JsonConvert.SerializeObject(customerModel);
//    var content = new StringContent(json, Encoding.UTF8, "application/json");

//    HttpResponseMessage response = await _httpClient.PostAsync("api/Customer/SignUpcustomer", content);

//    if (response.IsSuccessStatusCode)
//    {
//        TempData["SuccessMessage"] = "Signup successful. Please login.";
//        return RedirectToAction("Login");
//    }
//    else
//    {
//        string errorMessage = await response.Content.ReadAsStringAsync();
//        TempData["ErrorMessage"] = !string.IsNullOrEmpty(errorMessage) ? errorMessage : "Signup failed. Please try again.";
//        return View("Signup", customerModel);
//    }
//}


//#endregion
//public async Task<IActionResult> Login(string email, string password)
//{
//    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
//    {
//        TempData["ErrorMessage"] = "Please enter both email and password.";
//        return View();
//    }

//    // 🔹 Admin Login
//    if (email == "kr@gmail.com" && password == "krishna")
//    {
//        HttpContext.Session.SetString("UserRole", "Admin");
//        return RedirectToAction("DashboardUI", "Dashboard"); // Redirect Admin
//    }

//    var loginModel = new { Email = email, Password = password };
//    var json = JsonConvert.SerializeObject(loginModel);
//    var content = new StringContent(json, Encoding.UTF8, "application/json");

//    HttpResponseMessage response = await _httpClient.PostAsync("api/customer/Logincustomer", content);

//    if (response.IsSuccessStatusCode)
//    {
//        string token = await response.Content.ReadAsStringAsync();
//        HttpContext.Session.SetString("JWTToken", token);
//        HttpContext.Session.SetString("UserRole", "User"); // Ensure UserRole is set to "User"

//        return RedirectToAction("Home", "User"); // Redirect User
//    }
//    else
//    {
//        TempData["ErrorMessage"] = "Invalid email or password. Please try again.";
//        return View();
//    }
//}