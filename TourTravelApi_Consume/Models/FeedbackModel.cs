using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class FeedbackModel
    {
        public int? FeedbackID { get; set; }
        [Required(ErrorMessage ="Customer ID is required")]
        public int CustomerID { get; set; }
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Package ID is required.")]
        public int PackageID { get; set; }
        public string? PackageName { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string Comments { get; set; }

        [Required(ErrorMessage = "Feedback Date is required.")]
        [DataType(DataType.Date)]
        public DateTime FeedbackDate { get; set; }
    }
   
}
