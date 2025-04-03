using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TourTravelApi_Consume.Models;

namespace TourTravelApi_Consume.Controllers
{
    public class OfferController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public OfferController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }
        #region Offer List Getall
        public async Task<IActionResult> OfferList()
        {
            //var response = await _httpClient.GetAsync("api/Offer");
            //if (response.IsSuccessStatusCode)
            //{
            //    var data = await response.Content.ReadAsStringAsync();
            //    var Offers = JsonConvert.DeserializeObject<List<OfferModel>>(data);
            //    return View(Offers);
            //}
            //return View(new List<OfferModel>());
            List<OfferModel> Offers = new List<OfferModel>();
            List<OfferModel> newOffers = new List<OfferModel>();

            try
            {
                // Make the HTTP GET request
                HttpResponseMessage response = await _httpClient.GetAsync("api/Offer");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string data = await response.Content.ReadAsStringAsync();

                    // Directly deserialize the JSON array into a List<OfferModel>
                    newOffers = JsonConvert.DeserializeObject<List<OfferModel>>(data);

                    Offers = newOffers;

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
            Console.WriteLine(Offers);
            return View(Offers);
        }
        #endregion
        #region Offer Add Edit 
        public async Task<IActionResult> OfferAddEdit(int? OfferID)
        {
            if (OfferID.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/Offer/{OfferID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Offer = JsonConvert.DeserializeObject<OfferModel>(data);
                    return View(Offer);
                }
            }
            return View(new OfferModel());
        }
        #endregion
        #region Offer Save
        [HttpPost]

        public async Task<IActionResult> Save(OfferModel offerModel)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, return the offer edit view
                return View("OfferAddEdit", offerModel);
            }

            var json = JsonConvert.SerializeObject(offerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;

            if (offerModel.OfferID == null)
                response = await _httpClient.PostAsync("api/Offer", content);
            else
                response = await _httpClient.PutAsync($"api/Offer/{offerModel.OfferID}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = offerModel.OfferID == null
                    ? "Offer successfully added."
                    : "Offer successfully updated.";
            }
            else
            {
                TempData["ErrorMessage"] = "Operation failed. Please try again.";
            }

            return RedirectToAction("OfferList");
        }
        //public async Task<IActionResult> Save(OfferModel Offer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var json = JsonConvert.SerializeObject(Offer);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response;

        //        if (Offer.OfferID == null)
        //            response = await _httpClient.PostAsync("api/Offer", content);
        //        else
        //            response = await _httpClient.PutAsync($"api/Offer/{Offer.OfferID}", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["SuccessMessage"] = Offer.OfferID == null
        //                ? "Offer successfully added."
        //                : "Offer successfully updated.";
        //        }
        //        else
        //        {
        //            TempData["SuccessMessage"] = "Operation failed. Please try again.";
        //        }

        //        return RedirectToAction("OfferList");
        //    }
        //    return View("OfferAddEdit", Offer);
        //}
        #endregion
        #region Offer Delete
        public async Task<IActionResult> Delete(int OfferID)
        {
            var response = await _httpClient.DeleteAsync($"api/Offer/{OfferID}");
            if (response.IsSuccessStatusCode)
            {
                TempData["DeleteSuccessMessage"] = "Offer deleted successfully.";
            }
            else
            {
                TempData["DeleteSuccessMessage"] = "Failed to delete the Offer.";
            }
            return RedirectToAction("OfferList");
        }
        #endregion
    }
}
