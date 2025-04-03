namespace TourTravelApi_Creation.Models
{
    public class BookingModel
    {
        public int? BookingID { get; set; }
        public int CustomerID { get; set; }
        public string? FullName { get; set; }
        public int PackageID { get; set; }
        public string? PackageName { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime TravelDate { get; set; }

        public int NumberOfPeople { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
    public class CustomerDropDownModel
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
    }
    public class PackageDropDownModel
    {
        public int PackageID { get; set; }
        public string PackageName { get; set; }
    }
}
