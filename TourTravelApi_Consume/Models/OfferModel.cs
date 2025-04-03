using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class OfferModel
    {
        public int? OfferID { get; set; }

        [Required(ErrorMessage = "Offer Name is required")]
        //[StringLength(100, ErrorMessage = "Offer Name cannot exceed 100 characters")]
        public string OfferName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Discount Percentage is required")]
        [Range(0, 100, ErrorMessage = "Discount Percentage must be between 0 and 100")]
        public decimal DiscountPercentage { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Applicable Packages are required")]
        [StringLength(200, ErrorMessage = "Applicable Packages cannot exceed 200 characters")]
        public string ApplicablePackages { get; set; }
    }
}
