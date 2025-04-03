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
    public class ItineraryController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ItineraryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }
        #region Itinerary List Getall
        public async Task<IActionResult> ItineraryList()
        {
            //var response = await _httpClient.GetAsync("api/Itinerary");
            //if (response.IsSuccessStatusCode)
            //{
            //    var data = await response.Content.ReadAsStringAsync();
            //    var Itinerarys = JsonConvert.DeserializeObject<List<ItinareryModel>>(data);
            //    return View(Itinerarys);
            //}
            //return View(new List<ItinareryModel>());
            List<ItineraryModel> Itinerarys = new List<ItineraryModel>();
            List<ItineraryModel> newItinerarys = new List<ItineraryModel>();

            try
            {
                // Make the HTTP GET request
                HttpResponseMessage response = await _httpClient.GetAsync("api/Itinerary");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string data = await response.Content.ReadAsStringAsync();

                    // Directly deserialize the JSON array into a List<ItinareryModel>
                    newItinerarys = JsonConvert.DeserializeObject<List<ItineraryModel>>(data);

                    Itinerarys = newItinerarys;

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
            Console.WriteLine(Itinerarys);
            return View(Itinerarys);
        }
        #endregion
        #region Itinerary Add Edit 
        public async Task<IActionResult> ItineraryAddEdit(int? ItineraryID)
        {
            await LoadPackageList();

            if (ItineraryID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Itinerary/{ItineraryID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Itinerary = JsonConvert.DeserializeObject<ItineraryModel>(data);
                    return View(Itinerary);
                }
            }
            return View(new ItineraryModel());
        }
        #endregion
        #region Itinerary Save
        [HttpPost]


        public async Task<IActionResult> Save(ItineraryModel ItineraryModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ItineraryAddEdit", ItineraryModel);
            }

            // 🚨 Fix: Convert null ItineraryID to 0
            ItineraryModel.ItineraryID ??= 0; // If null, set to 0

            // Debugging: Check what ID is being passed
            Console.WriteLine($"ItineraryID received in Save method: {ItineraryModel.ItineraryID}");

            var json = JsonConvert.SerializeObject(ItineraryModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;

            try
            {
                if (ItineraryModel.ItineraryID == 0) // New Record
                {
                    Console.WriteLine("Performing INSERT operation...");
                    response = await _httpClient.PostAsync("api/Itinerary", content);
                }
                else // Update existing
                {
                    Console.WriteLine("Performing UPDATE operation...");
                    response = await _httpClient.PutAsync($"api/Itinerary/{ItineraryModel.ItineraryID}", content);
                }

                // Read API response
                string responseMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {responseMessage}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = ItineraryModel.ItineraryID == 0
                        ? "Itinerary successfully added."
                        : "Itinerary successfully updated.";
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

            return RedirectToAction("ItineraryList");
        }

        //public async Task<IActionResult> Save(ItineraryModel itineraryModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        await LoadPackageList();
        //        return View("ItineraryAddEdit", itineraryModel);
        //    }

        //    var json = JsonConvert.SerializeObject(itineraryModel);
        //    Console.WriteLine("Request JSON: " + json); 
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response;

        //    if (itineraryModel.ItineraryID == null)
        //        response = await _httpClient.PostAsync("api/Itinerary", content);
        //    else
        //        response = await _httpClient.PutAsync($"api/Itinerary/{itineraryModel.ItineraryID}", content);

        //    string responseMessage = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine($"Response Code: {response.StatusCode}, Response Message: {responseMessage}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        TempData["SuccessMessage"] = itineraryModel.ItineraryID == null
        //            ? "Itinerary successfully added."
        //            : "Itinerary successfully updated.";
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = $"Operation failed: {responseMessage}";
        //    }

        //    return RedirectToAction("ItineraryList");
        //}
        #endregion
        #region Itinerary Delete
        public async Task<IActionResult> Delete(int ItineraryID)
        {
            var response = await _httpClient.DeleteAsync($"api/Itinerary/{ItineraryID}");
            if (response.IsSuccessStatusCode)
            {
                TempData["DeleteSuccessMessage"] = "Itinerary deleted successfully.";
            }
            else
            {
                TempData["DeleteSuccessMessage"] = "Failed to delete the Itinerary.";
            }
            return RedirectToAction("ItineraryList");
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
    }
}
