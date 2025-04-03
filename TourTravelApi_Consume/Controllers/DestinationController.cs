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
    public class DestinationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public DestinationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }
        #region Destination List Getall
        public async Task<IActionResult> DestinationList()
        {
            //var response = await _httpClient.GetAsync("api/Destination");
            //if (response.IsSuccessStatusCode)
            //{
            //    var data = await response.Content.ReadAsStringAsync();
            //    var Destinations = JsonConvert.DeserializeObject<List<DestinationModel>>(data);
            //    return View(Destinations);
            //}
            //return View(new List<DestinationModel>());
            List<DestinationModel> Destinations = new List<DestinationModel>();
            List<DestinationModel> newDestinations = new List<DestinationModel>();

            try
            {
                // Make the HTTP GET request
                HttpResponseMessage response = await _httpClient.GetAsync("api/Destination");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string data = await response.Content.ReadAsStringAsync();

                    // Directly deserialize the JSON array into a List<DestinationModel>
                    newDestinations = JsonConvert.DeserializeObject<List<DestinationModel>>(data);

                    Destinations = newDestinations;

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
            Console.WriteLine(Destinations);
            return View(Destinations);
        }
        #endregion
        #region Destination Add Edit 
        public async Task<IActionResult> DestinationAddEdit(int? DestinationID)
        {
            if (DestinationID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Destination/{DestinationID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Destination = JsonConvert.DeserializeObject<DestinationModel>(data);
                    return View(Destination);
                }
            }
            return View(new DestinationModel());
        }
        #endregion
        #region Destination Save
        [HttpPost]

        public async Task<IActionResult> Save(DestinationModel destinationModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View("DestinationAddEdit", destinationModel);
            }

                var json = JsonConvert.SerializeObject(destinationModel, Formatting.Indented);
                Console.WriteLine("Serialized JSON: " + json);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                if (destinationModel.DestinationID == null || destinationModel.DestinationID == 0)
                    response = await _httpClient.PostAsync("api/Destination", content);
                else
                    response = await _httpClient.PutAsync($"api/Destination/{destinationModel.DestinationID}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = destinationModel.DestinationID == null
                        ? "Destination successfully added."
                        : "Destination successfully updated.";
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Error Response Content: " + errorContent);
                    TempData["ErrorMessage"] = "Operation failed. Server responded with an error.";
                }
            

            return RedirectToAction("DestinationList");
        }
        //public async Task<IActionResult> Save(DestinationModel destination)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var json = JsonConvert.SerializeObject(destination);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response;

        //        if (destination.DestinationID == null)
        //            response = await _httpClient.PostAsync("api/Destination", content);
        //        else
        //            response = await _httpClient.PutAsync($"api/Destination/{destination.DestinationID}", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["SuccessMessage"] = destination.DestinationID == null
        //                ? "Destination successfully added."
        //                : "Destination successfully updated.";
        //        }
        //        else
        //        {
        //            TempData["SuccessMessage"] = "Operation failed. Please try again.";
        //        }

        //        return RedirectToAction("DestinationList");
        //    }
        //    return View("DestinationAddEdit", destination);
        //}
        #endregion
        #region Destination Delete
        public async Task<IActionResult> Delete(int DestinationID)
        {
            var response = await _httpClient.DeleteAsync($"api/Destination/{DestinationID}");
            if (response.IsSuccessStatusCode)
            {
                TempData["DeleteSuccessMessage"] = "Destination deleted successfully.";
            }
            else
            {
                TempData["DeleteSuccessMessage"] = "Failed to delete the Destination.";
            }
            return RedirectToAction("DestinationList");
        }
        #endregion
    }
}

//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Text;
//using TourTravelApi_Consume.Models;

//namespace TourTravelApi_Consume.Controllers
//{
//    public class DestinationController : Controller
//    {
//        private readonly IConfiguration _configuration;
//        private readonly HttpClient _httpClient;
//        public DestinationController(IConfiguration configuration)
//        {
//            _configuration = configuration;
//            _httpClient = new HttpClient
//            {
//                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
//            };
//        }

//        #region Destination List Getall
//        public async Task<IActionResult> DestinationList()
//        {
//            //var response = await _httpClient.GetAsync("api/Destination");
//            //if (response.IsSuccessStatusCode)
//            //{
//            //    var data = await response.Content.ReadAsStringAsync();
//            //    var Destinations = JsonConvert.DeserializeObject<List<DestinationModel>>(data);
//            //    return View(Destinations);
//            //}
//            //return View(new List<DestinationModel>());
//            List<DestinationModel> destinations = new List<DestinationModel>();
//            List<DestinationModel> newDestinations = new List<DestinationModel>();

//            try
//            {
//                // Make the HTTP GET request
//                HttpResponseMessage response = await _httpClient.GetAsync("api/destination");

//                if (response.IsSuccessStatusCode)
//                {
//                    // Read the response content
//                    string data = await response.Content.ReadAsStringAsync();

//                    // Directly deserialize the JSON array into a List<DestinationModel>
//                    newDestinations = JsonConvert.DeserializeObject<List<DestinationModel>>(data);

//                    destinations = newDestinations;

//                }
//                else
//                {
//                    // Handle unsuccessful responses
//                    Console.WriteLine($"API Error: {response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log or handle exceptions
//                Console.WriteLine($"Exception occurred: {ex.Message}");
//            }
//            Console.WriteLine(destinations);
//            return View(destinations);
//        }
//        #endregion
//        #region Destination Add Edit 
//        public async Task<IActionResult> DestinationAddEdit(int? DestinationID)
//        {
//            if (DestinationID.HasValue)
//            {
//                var response = await _httpClient.GetAsync($"api/destination/{DestinationID}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var data = await response.Content.ReadAsStringAsync();
//                    var destination = JsonConvert.DeserializeObject<DestinationModel>(data);
//                    return View(destination);
//                }
//            }
//            return View(new DestinationModel());
//        }
//        #endregion
//        #region Destination Save
//        [HttpPost]
//        [HttpPost]
//        public async Task<IActionResult> Save(DestinationModel destination)
//        {
//            if (ModelState.IsValid)
//            {
//                var json = JsonConvert.SerializeObject(destination);
//                var content = new StringContent(json, Encoding.UTF8, "application/json");

//                HttpResponseMessage response;

//                try
//                {
//                    // Decide the HTTP method based on the presence of DestinationID
//                    if (destination.DestinationID == null)
//                    {
//                        response = await _httpClient.PostAsync("api/Destination", content);
//                    }
//                    else
//                    {
//                        response = await _httpClient.PutAsync($"api/Destination/{destination.DestinationID}", content);
//                    }

//                    if (response.IsSuccessStatusCode)
//                    {
//                        TempData["SuccessMessage"] = destination.DestinationID == null
//                            ? "Destination successfully added."
//                            : "Destination successfully updated.";
//                    }
//                    else
//                    {
//                        TempData["SuccessMessage"] = $"Operation failed. Status Code: {response.StatusCode}";
//                        // Log the failure response
//                        Console.WriteLine($"API failed with status: {response.StatusCode}, {response.ReasonPhrase}");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    TempData["SuccessMessage"] = "An error occurred. Please try again.";
//                    Console.WriteLine($"Exception: {ex.Message}");
//                }

//                return RedirectToAction("DestinationList");
//            }

//            // If ModelState is invalid, return to the same view with the data
//            return View("DestinationAddEdit", destination);
//        }
//        #endregion
//        #region Destination Delete
//        public async Task<IActionResult> Delete(int DestinationID)
//        {
//            var response = await _httpClient.DeleteAsync($"api/destination/{DestinationID}");
//            if (response.IsSuccessStatusCode)
//            {
//                TempData["DeleteSuccessMessage"] = "Destination deleted successfully.";
//            }
//            else
//            {
//                TempData["DeleteSuccessMessage"] = "Failed to delete the Destination.";
//            }
//            return RedirectToAction("DestinationList");
//        }
//        #endregion
//    }
//}
