namespace TourTravelApi_Creation.Models
{
    public class PaymentModel
    {
        public int? PaymentID { get; set; }
        public int BookingID { get; set; }
        public DateTime? BookingDate{get;set;}
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentStatus { get; set; }
    }
    public class BookingDropDownModel
    {
        public int BookingID { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
