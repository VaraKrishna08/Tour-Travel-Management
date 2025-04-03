using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class PackageModel
    {  public int? PackageID { get; set; }
        [Required(ErrorMessage = "Package Name is required.")]
        [StringLength(100, ErrorMessage = "Package Name cannot exceed 100 characters.")]
        public string PackageName { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Destination ID is required.")]
        public int DestinationID { get; set; }
        public string? DestinationName { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 day.")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; }
    }
    public class DestinationDropDownModel
    {
        public int DestinationID { get; set; }
        public string DestinationName { get; set; }
    }
}
