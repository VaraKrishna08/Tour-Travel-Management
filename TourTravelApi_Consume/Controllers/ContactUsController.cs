//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using TourTravelApi_Consume.Models;
//namespace TourTravelApi_Consume.Controllers
//{
//    public class ContactUsController : Controller
//    {
//        private readonly AppDbContext _context;

//        public ContactUsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpPost]
//        public async Task<IActionResult> SubmitForm([FromBody] ContactUsModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.ContactUs.Add(model);
//                await _context.SaveChangesAsync();
//                return Json(new { success = true });
//            }
//            return Json(new { success = false, message = "Invalid data" });
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourTravelApi_Consume.Models;

namespace TourTravelApi_Consume.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactUsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm(ContactUsModel model)  // ✅ Remove [FromBody]
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Invalid request. Model is null." });
            }

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Validation failed.", errors = ModelState });
            }

            try
            {
                _context.ContactUs.Add(model);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Your message has been submitted!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Database error.", error = ex.Message });
            }
        }
    }
}
