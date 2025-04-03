using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using TourTravelApi_Consume.Models;

namespace TourTravelApi_Consume.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerModel>> GetCustomersAsync()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5241/api/Customer");
            return JsonConvert.DeserializeObject<List<CustomerModel>>(response);
        }

        public async Task<List<BookingModel>> GetBookingsAsync()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5241/api/Booking");
            return JsonConvert.DeserializeObject<List<BookingModel>>(response);
        }

        public async Task<List<FeedbackModel>> GetFeedbackAsync()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5241/api/Feedback");
            return JsonConvert.DeserializeObject<List<FeedbackModel>>(response);
        }

        public async Task<List<DestinationModel>> GetDestinationAsync()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5241/api/Destination");
            return JsonConvert.DeserializeObject<List<DestinationModel>>(response);
        }

        public async Task<List<PackageModel>> GetPackageAsync()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5241/api/Package");
            return JsonConvert.DeserializeObject<List<PackageModel>>(response);
        }
    }
}
