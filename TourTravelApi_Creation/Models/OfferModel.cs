namespace TourTravelApi_Creation.Models
{
    public class OfferModel
    {
        public int? OfferID { get; set; }
        public string OfferName { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ApplicablePackages { get; set; }


}
}
