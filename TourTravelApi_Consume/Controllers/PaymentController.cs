using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Consume.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TourTravelApi_Consume.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        #region Payment List Getall
        public async Task<IActionResult> PaymentList()
        {
            List<PaymentModel> payments = new List<PaymentModel>();
            try
            {
                // Make the HTTP GET request
                HttpResponseMessage response = await _httpClient.GetAsync("api/Payment");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string data = await response.Content.ReadAsStringAsync();

                    // Directly deserialize the JSON array into a List<PaymentModel>
                    payments = JsonConvert.DeserializeObject<List<PaymentModel>>(data);
                }
                else
                {
                    // Handle unsuccessful responses
                    Console.WriteLine($"API Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }

            return View(payments);
        }
        #endregion

        #region Payment Add Edit 
        public async Task<IActionResult> PaymentAddEdit(int? PaymentID)
        {
            await LoadBookingList();

            if (PaymentID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Payment/{PaymentID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Payment = JsonConvert.DeserializeObject<PaymentModel>(data);
                    return View(Payment);
                }
            }
            return View(new PaymentModel());
        }
        #endregion

        #region Payment Save
        [HttpPost]
        public async Task<IActionResult> Save(PaymentModel paymentModel)
        {
            if (!ModelState.IsValid)
            {
                await LoadBookingList();
                return View("PaymentAddEdit", paymentModel);
            }

            var json = JsonConvert.SerializeObject(paymentModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;

            if (paymentModel.PaymentID == null)
                response = await _httpClient.PostAsync("api/Payment", content);
            else
                response = await _httpClient.PutAsync($"api/Payment/{paymentModel.PaymentID}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = paymentModel.PaymentID == null
                    ? "Payment successfully added."
                    : "Payment successfully updated.";
            }
            else
            {
                TempData["SuccessMessage"] = "Operation failed. Please try again.";
            }

            return RedirectToAction("PaymentList");
        }
        #endregion

        #region Payment Delete
        public async Task<IActionResult> Delete(int PaymentID)
        {
            var response = await _httpClient.DeleteAsync($"api/Payment/{PaymentID}");
            if (response.IsSuccessStatusCode)
            {
                TempData["DeleteSuccessMessage"] = "Payment deleted successfully.";
            }
            else
            {
                TempData["DeleteSuccessMessage"] = "Failed to delete the Payment.";
            }
            return RedirectToAction("PaymentList");
        }
        #endregion

        private async Task LoadBookingList()
        {
            var response = await _httpClient.GetAsync("api/Payment/Bookings");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var bookings = JsonConvert.DeserializeObject<List<BookingDropDownModel>>(data);
                ViewBag.BookingList = bookings;
            }
        }
    }
}
//using Microsoft.AspNetCore.Mvc;
//using TourTravelApi_Consume.Models;
//using System.Text;
//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Diagnostics.Metrics;
//namespace TourTravelApi_Consume.Controllers
//{
//    public class PaymentController : Controller
//    {
//            private readonly IConfiguration _configuration;
//            private readonly HttpClient _httpClient;

//            public PaymentController(IConfiguration configuration)
//            {
//                _configuration = configuration;
//                _httpClient = new HttpClient
//                {
//                    BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
//                };
//            }
//            #region Payment List Getall
//            public async Task<IActionResult> PaymentList()
//            {
//                //var response = await _httpClient.GetAsync("api/Payment");
//                //if (response.IsSuccessStatusCode)
//                //{
//                //    var data = await response.Content.ReadAsStringAsync();
//                //    var Payments = JsonConvert.DeserializeObject<List<ItinareryModel>>(data);
//                //    return View(Payments);
//                //}
//                //return View(new List<ItinareryModel>());
//                List<PaymentModel> payments = new List<PaymentModel>();
//                List<PaymentModel> newPayments = new List<PaymentModel>();

//                try
//                {
//                    // Make the HTTP GET request
//                    HttpResponseMessage response = await _httpClient.GetAsync("api/Payment");

//                    if (response.IsSuccessStatusCode)
//                    {
//                        // Read the response content
//                        string data = await response.Content.ReadAsStringAsync();

//                        // Directly deserialize the JSON array into a List<ItinareryModel>
//                        newPayments = JsonConvert.DeserializeObject<List<PaymentModel>>(data);

//                        payments = newPayments;

//                    }
//                    else
//                    {
//                        // Handle unsuccessful responses
//                        Console.WriteLine($"API Error: {response.StatusCode}");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    // Log or handle exceptions
//                    Console.WriteLine($"Exception occurred: {ex.Message}");
//                }
//                Console.WriteLine(payments);
//                return View(payments);
//            }
//            #endregion
//            #region Payment Add Edit 
//            public async Task<IActionResult> PaymentAddEdit(int? PaymentID)
//            {
//                await LoadBookingList();

//                if (PaymentID.HasValue)
//                {
//                    var response = await _httpClient.GetAsync($"api/Payment/{PaymentID}");
//                    if (response.IsSuccessStatusCode)
//                    {
//                        var data = await response.Content.ReadAsStringAsync();
//                        var Payment = JsonConvert.DeserializeObject<PaymentModel>(data);
//                        return View(Payment);
//                    }
//                }
//                return View(new PaymentModel());
//            }
//            #endregion
//            #region Payment Save
//            [HttpPost]
//        [HttpPost]
//        public async Task<IActionResult> Save(PaymentModel paymentModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                await LoadBookingList();
//                return View("PaymentAddEdit", paymentModel);
//            }

//            var json = JsonConvert.SerializeObject(paymentModel);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");
//            HttpResponseMessage response;

//            if (paymentModel.PaymentID == null)
//                response = await _httpClient.PostAsync("api/Payment", content);
//            else
//                response = await _httpClient.PutAsync($"api/Payment/{paymentModel.PaymentID}", content);

//            if (response.IsSuccessStatusCode)
//            {
//                TempData["SuccessMessage"] = paymentModel.PaymentID == null
//                    ? "Payment successfully added."
//                    : "Payment successfully updated.";
//            }
//            else
//            {
//                TempData["SuccessMessage"] = "Operation failed. Please try again.";
//            }

//            return RedirectToAction("PaymentList");
//        }

//        //public async Task<IActionResult> Save(PaymentModel PaymentModel)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        var json = JsonConvert.SerializeObject(PaymentModel);
//        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
//        //        HttpResponseMessage response;

//        //        if (PaymentModel.PaymentID == null)
//        //            response = await _httpClient.PostAsync("api/Payment", content);
//        //        else
//        //            response = await _httpClient.PutAsync($"api/Payment/{PaymentModel.PaymentID}", content);

//        //        if (response.IsSuccessStatusCode)
//        //        {
//        //            TempData["SuccessMessage"] = PaymentModel.PaymentID == null
//        //                ? "Payment successfully added."
//        //                : "Payment successfully updated.";
//        //        }
//        //        else
//        //        {
//        //            TempData["SuccessMessage"] = "Operation failed. Please try again.";
//        //        }

//        //        return RedirectToAction("PaymentList");
//        //    }
//        //    await LoadBookingList();
//        //    return View("PaymentAddEdit", PaymentModel);
//        //}
//        #endregion
//        #region Payment Delete
//        public async Task<IActionResult> Delete(int PaymentID)
//            {
//                var response = await _httpClient.DeleteAsync($"api/Payment/{PaymentID}");
//                if (response.IsSuccessStatusCode)
//                {
//                    TempData["DeleteSuccessMessage"] = "Payment deleted successfully.";
//                }
//                else
//                {
//                    TempData["DeleteSuccessMessage"] = "Failed to delete the Payment.";
//                }
//                return RedirectToAction("PaymentList");
//            }
//            #endregion

//            private async Task LoadBookingList()
//            {
//                var response = await _httpClient.GetAsync("api/Payment/Bookings");
//                if (response.IsSuccessStatusCode)
//                {
//                    var data = await response.Content.ReadAsStringAsync();
//                    var bookings = JsonConvert.DeserializeObject<List<BookingDropDownModel>>(data);
//                    ViewBag.BookingList = bookings;
//                }
//            }
//        }
//}