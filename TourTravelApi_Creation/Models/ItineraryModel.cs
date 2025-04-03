namespace TourTravelApi_Creation.Models
{
    public class ItineraryModel
    {
        public int? ItineraryID { get; set; }
        public int PackageID { get; set; }
        public string? PackageName { get; set; }
        public string ImageUrl { get; set; }
        public int DayNumber { get; set; }
        public string Activity { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
    }
}
