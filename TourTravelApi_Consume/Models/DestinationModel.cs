using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class DestinationModel
    {
        public int? DestinationID { get; set; }

        [Required(ErrorMessage = "Destination Name is required")]
        [StringLength(100, ErrorMessage = "Destination Name can't be longer than 100 characters")]
        public string DestinationName { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, ErrorMessage = "Country name can't be longer than 100 characters")]
        public string Country { get; set; }

        [StringLength(100, ErrorMessage = "State name can't be longer than 100 characters")]
        public string State { get; set; }

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Best time to visit is required")]
        [StringLength(100, ErrorMessage = "Best time to visit can't be longer than 100 characters")]
        public string BestTimeToVisit { get; set; }
    }
}
