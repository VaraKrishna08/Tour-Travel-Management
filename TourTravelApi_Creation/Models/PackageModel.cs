namespace TourTravelApi_Creation.Models
{
    public class PackageModel
    {
        public int? PackageID { get; set; }
        public string PackageName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int DestinationID { get; set; }
        public string? DestinationName { get; set; } // Added DestinationName property

        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string Status {get;set;}
    }
    public class DestinationDropDownModel
    {
        public int DestinationID{get;set;}
        public string DestinationName{get;set;}
    }
}
