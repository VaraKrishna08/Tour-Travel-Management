namespace TourTravelApi_Creation.Models
{
    public class FeedbackModel
    {
        public int? FeedbackID { get; set; }
        public int CustomerID { get; set; }
        public string? FullName { get; set; }
        public int PackageID { get; set; }
        public string? PackageName { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}
