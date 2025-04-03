using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class ContactUsModel
    {
        [Key]
        public int? ContactID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
