using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TourTravelApi_Consume.Models;

namespace TourTravelApi_Consume.Controllers
{
    public class PackageController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public PackageController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }
        #region Package List Getall
        public async Task<IActionResult> PackageList()
        {
            //var response = await _httpClient.GetAsync("api/Package");
            //if (response.IsSuccessStatusCode)
            //{
            //    var data = await response.Content.ReadAsStringAsync();
            //    var Packages = JsonConvert.DeserializeObject<List<ItinareryModel>>(data);
            //    return View(Packages);
            //}
            //return View(new List<ItinareryModel>());
            List<PackageModel> Packages = new List<PackageModel>();
            List<PackageModel> newPackages = new List<PackageModel>();

            try
            {
                // Make the HTTP GET request
                HttpResponseMessage response = await _httpClient.GetAsync("api/Package");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string data = await response.Content.ReadAsStringAsync();

                    // Directly deserialize the JSON array into a List<ItinareryModel>
                    newPackages = JsonConvert.DeserializeObject<List<PackageModel>>(data);

                    Packages = newPackages;

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
            Console.WriteLine(Packages);
            return View(Packages);
        }
        #endregion
        #region Package Add Edit 
        public async Task<IActionResult> PackageAddEdit(int? PackageID)
        {
            await LoadDestinationList();

            if (PackageID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Package/{PackageID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Package = JsonConvert.DeserializeObject<PackageModel>(data);
                    return View(Package);
                }
            }
            return View(new PackageModel());
        }
        #endregion
        #region Package Save
        [HttpPost]

        public async Task<IActionResult> Save(PackageModel packageModel)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, reload destination list and return the view
                await LoadDestinationList();
                return View("PackageAddEdit", packageModel);
            }

            var json = JsonConvert.SerializeObject(packageModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;

            if (packageModel.PackageID == null)
                response = await _httpClient.PostAsync("api/Package", content);
            else
                response = await _httpClient.PutAsync($"api/Package/{packageModel.PackageID}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = packageModel.PackageID == null
                    ? "Package successfully added."
                    : "Package successfully updated.";
            }
            else
            {
                TempData["ErrorMessage"] = "Operation failed. Please try again.";
            }

            return RedirectToAction("PackageList");
        }
        //public async Task<IActionResult> Save(PackageModel PackageModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var json = JsonConvert.SerializeObject(PackageModel);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response;

        //        if (PackageModel.PackageID == null)
        //            response = await _httpClient.PostAsync("api/Package", content);
        //        else
        //            response = await _httpClient.PutAsync($"api/Package/{PackageModel.PackageID}", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["SuccessMessage"] = PackageModel.PackageID == null
        //                ? "Package successfully added."
        //                : "Package successfully updated.";
        //        }
        //        else
        //        {
        //            TempData["SuccessMessage"] = "Operation failed. Please try again.";
        //        }

        //        return RedirectToAction("PackageList");
        //    }
        //    await LoadDestinationList();
        //    return View("PackageAddEdit", PackageModel);
        //}
        #endregion
        #region Package Delete
        public async Task<IActionResult> Delete(int PackageID)
        {
            var response = await _httpClient.DeleteAsync($"api/Package/{PackageID}");
            if (response.IsSuccessStatusCode)
            {
                TempData["DeleteSuccessMessage"] = "Package deleted successfully.";
            }
            else
            {
                TempData["DeleteSuccessMessage"] = "Failed to delete the Package.";
            }
            return RedirectToAction("PackageList");
        }
        #endregion

        private async Task LoadDestinationList()
        {
            var response = await _httpClient.GetAsync("api/Destination");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var destination = JsonConvert.DeserializeObject<List<DestinationDropDownModel>>(data);
                ViewBag.DestinationList = destination;
            }
        }
    }
}
