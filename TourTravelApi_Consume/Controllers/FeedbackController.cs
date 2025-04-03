using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Consume.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Diagnostics.Metrics;
namespace TourTravelApi_Consume.Controllers
{
    public class FeedbackController : Controller
    {
            private readonly IConfiguration _configuration;
            private readonly HttpClient _httpClient;

            public FeedbackController(IConfiguration configuration)
            {
                _configuration = configuration;
                _httpClient = new HttpClient
                {
                    BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
                };
            }
            #region Feedback List Getall
            public async Task<IActionResult> FeedbackList()
            {
                //var response = await _httpClient.GetAsync("api/Feedback");
                //if (response.IsSuccessStatusCode)
                //{
                //    var data = await response.Content.ReadAsStringAsync();
                //    var Feedbacks = JsonConvert.DeserializeObject<List<ItinareryModel>>(data);
                //    return View(Feedbacks);
                //}
                //return View(new List<ItinareryModel>());
                List<FeedbackModel> Feedbacks = new List<FeedbackModel>();
                List<FeedbackModel> newFeedbacks = new List<FeedbackModel>();

                try
                {
                    // Make the HTTP GET request
                    HttpResponseMessage response = await _httpClient.GetAsync("api/Feedback");

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string data = await response.Content.ReadAsStringAsync();

                        // Directly deserialize the JSON array into a List<ItinareryModel>
                        newFeedbacks = JsonConvert.DeserializeObject<List<FeedbackModel>>(data);

                        Feedbacks = newFeedbacks;

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
                Console.WriteLine(Feedbacks);
                return View(Feedbacks);
            }
            #endregion
            #region Feedback Add Edit 
            public async Task<IActionResult> FeedbackAddEdit(int? FeedbackID)
            {
                await LoadPackageList();
                await LoadCustomerList();
                if (FeedbackID.HasValue)
                {
                    var response = await _httpClient.GetAsync($"api/Feedback/{FeedbackID}");
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var Feedback = JsonConvert.DeserializeObject<FeedbackModel>(data);
                        return View(Feedback);
                    }
                }
                return View(new FeedbackModel());
            }
            #endregion
            #region Feedback Save
            [HttpPost]
        public async Task<IActionResult> Save(FeedbackModel feedbackModel)
        {
            if (!ModelState.IsValid)
            {
                
                await LoadPackageList();
                await LoadCustomerList();
                return View("FeedbackAddEdit", feedbackModel);
            }

            var json = JsonConvert.SerializeObject(feedbackModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;

                if (feedbackModel.FeedbackID == null)
                    response = await _httpClient.PostAsync("api/Feedback", content);
                else
                    response = await _httpClient.PutAsync($"api/Feedback/{feedbackModel.FeedbackID}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = feedbackModel.FeedbackID == null
                        ? "Feedback successfully added."
                        : "Feedback successfully updated.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Operation failed. Please try again.";
                }
            
            return RedirectToAction("FeedbackList");
        }

            //public async Task<IActionResult> Save(FeedbackModel FeedbackModel)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        var json = JsonConvert.SerializeObject(FeedbackModel);
            //        var content = new StringContent(json, Encoding.UTF8, "application/json");
            //        HttpResponseMessage response;

            //        if (FeedbackModel.FeedbackID == null)
            //            response = await _httpClient.PostAsync("api/Feedback", content);
            //        else
            //            response = await _httpClient.PutAsync($"api/Feedback/{FeedbackModel.FeedbackID}", content);

            //        if (response.IsSuccessStatusCode)
            //        {
            //            TempData["SuccessMessage"] = FeedbackModel.FeedbackID == null
            //                ? "Feedback successfully added."
            //                : "Feedback successfully updated.";
            //        }
            //        else
            //        {
            //            TempData["SuccessMessage"] = "Operation failed. Please try again.";
            //        }

            //        return RedirectToAction("FeedbackList");
            //    }
            //    await LoadPackageList();
            //    await LoadCustomerList();
            //    return View("FeedbackAddEdit", FeedbackModel);
            //}
        #endregion
        #region Feedback Delete
            public async Task<IActionResult> Delete(int FeedbackID)
            {
                var response = await _httpClient.DeleteAsync($"api/Feedback/{FeedbackID}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["DeleteSuccessMessage"] = "Feedback deleted successfully.";
                }
                else
                {
                    TempData["DeleteSuccessMessage"] = "Failed to delete the Feedback.";
                }
                return RedirectToAction("FeedbackList");
            }
            #endregion

            private async Task LoadPackageList()
            {
                var response = await _httpClient.GetAsync("api/Package");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var packages = JsonConvert.DeserializeObject<List<PackageDropDownModel>>(data);
                    ViewBag.PackageList = packages;
                }
            }
            private async Task LoadCustomerList()
            {
                var response = await _httpClient.GetAsync("api/Customer");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<List<CustomerDropDownModel>>(data);
                    ViewBag.CustomerList = customers;
                }
            }
        }
    }

