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
    public class BookingController : Controller
    {
        
            private readonly IConfiguration _configuration;
            private readonly HttpClient _httpClient;

            public BookingController(IConfiguration configuration)
            {
                _configuration = configuration;
                _httpClient = new HttpClient
                {
                    BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
                };
            }
            #region Booking List Getall
            public async Task<IActionResult> BookingList()
            {
                //var response = await _httpClient.GetAsync("api/Booking");
                //if (response.IsSuccessStatusCode)
                //{
                //    var data = await response.Content.ReadAsStringAsync();
                //    var Bookings = JsonConvert.DeserializeObject<List<ItinareryModel>>(data);
                //    return View(Bookings);
                //}
                //return View(new List<ItinareryModel>());
                List<BookingModel> Bookings = new List<BookingModel>();
                List<BookingModel> newBookings = new List<BookingModel>();

                try
                {
                    // Make the HTTP GET request
                    HttpResponseMessage response = await _httpClient.GetAsync("api/Booking");

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string data = await response.Content.ReadAsStringAsync();

                        // Directly deserialize the JSON array into a List<ItinareryModel>
                        newBookings = JsonConvert.DeserializeObject<List<BookingModel>>(data);

                        Bookings = newBookings;

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
                Console.WriteLine(Bookings);
                return View(Bookings);
            }
            #endregion
            #region Booking Add Edit 
            public async Task<IActionResult> BookingAddEdit(int? BookingID)
            {
                await LoadPackageList();
                 await LoadCustomerList();
                if (BookingID.HasValue)
                {
                    var response = await _httpClient.GetAsync($"api/Booking/{BookingID}");
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var Booking = JsonConvert.DeserializeObject<BookingModel>(data);
                        return View(Booking);
                    }
                }
                return View(new BookingModel());
            }
            #endregion
            #region Booking Save
            [HttpPost]
            public async Task<IActionResult> Save(BookingModel bookingModel)
            {
            if (!ModelState.IsValid)
            {
                await LoadPackageList();
                await LoadCustomerList();
                return View("BookingAddEdit", bookingModel);
            }
            var json = JsonConvert.SerializeObject(bookingModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;

                    if (bookingModel.BookingID == null)
                        response = await _httpClient.PostAsync("api/Booking", content);
                    else
                        response = await _httpClient.PutAsync($"api/Booking/{bookingModel.BookingID}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = bookingModel.BookingID == null
                            ? "Booking successfully added."
                            : "Booking successfully updated.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Operation failed. Please try again.";
                    }

                    return RedirectToAction("BookingList");
            }
             
            
            #endregion
            #region Booking Delete
            public async Task<IActionResult> Delete(int BookingID)
            {
                var response = await _httpClient.DeleteAsync($"api/Booking/{BookingID}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["DeleteSuccessMessage"] = "Booking deleted successfully.";
                }
                else
                {
                    TempData["DeleteSuccessMessage"] = "Failed to delete the Booking.";
                }
                return RedirectToAction("BookingList");
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


