using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Consume.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Diagnostics.Metrics;
using System;
namespace TourTravelApi_Consume.Controllers
{
    public class TransportationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public TransportationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }
        #region Transportation List Getall
        public async Task<IActionResult> TransportationList()
        {
            //var response = await _httpClient.GetAsync("api/Transportation");
            //if (response.IsSuccessStatusCode)
            //{
            //    var data = await response.Content.ReadAsStringAsync();
            //    var Transportations = JsonConvert.DeserializeObject<List<ItinareryModel>>(data);
            //    return View(Transportations);
            //}
            //return View(new List<ItinareryModel>());
            List<TransportationModel> Transportations = new List<TransportationModel>();
            List<TransportationModel> newTransportations = new List<TransportationModel>();

            try
            {
                // Make the HTTP GET request
                HttpResponseMessage response = await _httpClient.GetAsync("api/Transportation");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string data = await response.Content.ReadAsStringAsync();

                    // Directly deserialize the JSON array into a List<ItinareryModel>
                    newTransportations = JsonConvert.DeserializeObject<List<TransportationModel>>(data);

                    Transportations = newTransportations;

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
            Console.WriteLine(Transportations);
            return View(Transportations);
        }
        #endregion
        #region Transportation Add Edit 
        public async Task<IActionResult> TransportationAddEdit(int? TransportID)
        {
            await LoadBookingList();

            if (TransportID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Transportation/{TransportID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Transportation = JsonConvert.DeserializeObject<TransportationModel>(data);
                    return View(Transportation);
                }
            }
            return View(new TransportationModel());
        }
        #endregion
        #region Transportation Save
        [HttpPost]
        public async Task<IActionResult> Save(TransportationModel transportationModel)
        {
            if (!ModelState.IsValid)
            {
                await LoadBookingList();
                return View("TransportationAddEdit", transportationModel);
            }
            var json = JsonConvert.SerializeObject(transportationModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (transportationModel.TransportID == null)
                    response = await _httpClient.PostAsync("api/Transportation", content);
                else
                    response = await _httpClient.PutAsync($"api/Transportation/{transportationModel.TransportID}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = transportationModel.TransportID == null
                        ? "Transportation successfully added."
                        : "Transportation successfully updated.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Operation failed. Please try again.";
                }

                return RedirectToAction("TransportationList");
            }
        
        //public async Task<IActionResult> Save(TransportationModel TransportationModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var json = JsonConvert.SerializeObject(TransportationModel);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response;

        //        if (TransportationModel.TransportID == null)
        //            response = await _httpClient.PostAsync("api/Transportation", content);
        //        else
        //            response = await _httpClient.PutAsync($"api/Transportation/{TransportationModel.TransportID}", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["SuccessMessage"] = TransportationModel.TransportID == null
        //                ? "Transportation successfully added."
        //                : "Transportation successfully updated.";
        //        }
        //        else
        //        {
        //            TempData["SuccessMessage"] = "Operation failed. Please try again.";
        //        }

        //        return RedirectToAction("TransportationList");
        //    }
        //    await LoadBookingList();
        //    return View("TransportationAddEdit", TransportationModel);
        //}
        #endregion
        #region Transportation Delete
        public async Task<IActionResult> Delete(int TransportID)
        {
            var response = await _httpClient.DeleteAsync($"api/Transportation/{TransportID}");
            if (response.IsSuccessStatusCode)
            {
                TempData["DeleteSuccessMessage"] = "Transportation deleted successfully.";
            }
            else
            {
                TempData["DeleteSuccessMessage"] = "Failed to delete the Transportation.";
            }
            return RedirectToAction("TransportationList");
        }
        #endregion

        private async Task LoadBookingList()
        {
            var response = await _httpClient.GetAsync("api/Transportation/Bookings");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var bookings = JsonConvert.DeserializeObject<List<BookingDropDownModel>>(data);
                ViewBag.BookingList = bookings;
            }
        }
    }
}
