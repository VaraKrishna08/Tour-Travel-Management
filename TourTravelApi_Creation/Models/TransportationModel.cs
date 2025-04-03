namespace TourTravelApi_Creation.Models
{
    public class TransportationModel
    {
        public int? TransportID { get; set; }
        public int BookingID { get; set; }
        public DateTime? BookingDate { get; set; }

        public string TransportMode { get; set; }
        public string TransportDetails { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Cost { get; set; }
    }
}
