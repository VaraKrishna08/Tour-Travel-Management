namespace TourTravelApi_Creation.Models
{
    public class CustomerModel
    {
        
            public int? CustomerID { get; set; }
            public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public DateTime RegistrationDate { get; set; }
            public string Password { get; set; }
    }
}
