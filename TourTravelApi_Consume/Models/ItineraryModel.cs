using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class ItineraryModel
    {
        public int? ItineraryID { get; set; }
        [Required(ErrorMessage ="Packaged Id Is required")]
        public int PackageID { get; set; }
        public string? PackageName { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Day Number is required.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Day Number must be a positive integer.")]
        public int DayNumber { get; set; }

        [Required(ErrorMessage = "Activity is required.")]
        //[StringLength(100, ErrorMessage = "Activity name cannot exceed 100 characters.")]
        public string Activity { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        //[StringLength(150, ErrorMessage = "Location name cannot exceed 150 characters.")]
        public string Location { get; set; }

        //[Required(ErrorMessage = "Time is required.")]
        //[DataType(DataType.Time)]
        public DateTime Time { get; set; }

    }

    public class PackageDropDownModel
    {
        public int PackageID { get; set; }
        public string PackageName { get; set; }
    }
}
