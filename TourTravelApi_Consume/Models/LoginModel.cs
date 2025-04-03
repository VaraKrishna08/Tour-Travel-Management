using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class LoginModel
    {
         public string Email { get; set; }

         public string Password { get; set; }
    }
}
