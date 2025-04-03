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
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }
        #region Employee List Getall
        public async Task<IActionResult> EmployeeList()
        {
            //var response = await _httpClient.GetAsync("api/Employee");
            //if (response.IsSuccessStatusCode)
            //{
            //    var data = await response.Content.ReadAsStringAsync();
            //    var Employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(data);
            //    return View(Employees);
            //}
            //return View(new List<EmployeeModel>());
            List<EmployeeModel> Employees = new List<EmployeeModel>();
            List<EmployeeModel> newEmployees = new List<EmployeeModel>();

            try
            {
                // Make the HTTP GET request
                HttpResponseMessage response = await _httpClient.GetAsync("api/Employee");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string data = await response.Content.ReadAsStringAsync();

                    // Directly deserialize the JSON array into a List<EmployeeModel>
                    newEmployees = JsonConvert.DeserializeObject<List<EmployeeModel>>(data);

                    Employees = newEmployees;

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
            Console.WriteLine(Employees);
            return View(Employees);
        }
        #endregion
        #region Employee Add Edit 
        public async Task<IActionResult> EmployeeAddEdit(int? EmployeeID)
        {
            if (EmployeeID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Employee/{EmployeeID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Employee = JsonConvert.DeserializeObject<EmployeeModel>(data);
                    return View(Employee);
                }
            }
            return View(new EmployeeModel());
        }
        #endregion
        #region Employee Save
        [HttpPost]
        public async Task<IActionResult> Save(EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return View("EmployeeAddEdit", employeeModel);
            }
            var json = JsonConvert.SerializeObject(employeeModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (employeeModel.EmployeeID == null)
                    response = await _httpClient.PostAsync("api/Employee", content);
                else
                    response = await _httpClient.PutAsync($"api/Employee/{employeeModel.EmployeeID}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = employeeModel.EmployeeID == null
                        ? "Employee successfully added."
                        : "Employee successfully updated.";
                }
                else
                {
                    TempData["SuccessMessage"] = "Operation failed. Please try again.";
                }

                return RedirectToAction("EmployeeList");
            
        }
        #endregion
        #region Employee Delete
        public async Task<IActionResult> Delete(int EmployeeID)
        {
            var response = await _httpClient.DeleteAsync($"api/Employee/{EmployeeID}");
            if (response.IsSuccessStatusCode)
            {
                TempData["DeleteSuccessMessage"] = "Employee deleted successfully.";
            }
            else
            {
                TempData["DeleteSuccessMessage"] = "Failed to delete the Employee.";
            }
            return RedirectToAction("EmployeeList");
        }
        #endregion

    }
}
