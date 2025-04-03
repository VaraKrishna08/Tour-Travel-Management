using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingRepository _bookingRepository;

        public BookingController(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAllBookings()
        {
            var bookings = _bookingRepository.SelectAll();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            var booking = _bookingRepository.SelectByPK(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var isDeleted = _bookingRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertBooking([FromBody] BookingModel booking)
        {
            if (booking == null)
                return BadRequest();

            bool isInserted = _bookingRepository.Insert(booking);

            if (isInserted)
                return Ok(new { Message = "Booking inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the Booking.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, [FromBody] BookingModel booking)
        {
            if (booking == null || id != booking.BookingID)
                return BadRequest();

            var isUpdated = _bookingRepository.Update(booking);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        [HttpGet("Customers")]
        public IActionResult Getcustomers()
        {
            var customers = _bookingRepository.Getcustomers();
            if (!customers.Any())
                return NotFound("No customers found.");

            return Ok(customers);
        }

        [HttpGet("Packages")]
        public IActionResult Getpackages()
        {
            var packages = _bookingRepository.Getpackages();
            if (!packages.Any())
                return NotFound("No packages found.");

            return Ok(packages);
        }
    }
}
