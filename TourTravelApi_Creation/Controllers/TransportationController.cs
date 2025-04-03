using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationController : ControllerBase
    {
        private readonly TransportationRepository _transportationRepository;

        public TransportationController(TransportationRepository transportationRepository)
        {
            _transportationRepository = transportationRepository;
        }

        [HttpGet]
        public IActionResult GetAllTransportations()
        {
            var transportations = _transportationRepository.SelectAll();
            return Ok(transportations);
        }

        [HttpGet("{id}")]
        public IActionResult GetTransportationById(int id)
        {
            var transportation = _transportationRepository.SelectByPK(id);
            if (transportation == null)
            {
                return NotFound();
            }
            return Ok(transportation);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransportation(int id)
        {
            var isDeleted = _transportationRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertTransportation([FromBody] TransportationModel transportation)
        {
            if (transportation == null)
                return BadRequest();

            bool isInserted = _transportationRepository.Insert(transportation);

            if (isInserted)
                return Ok(new { Message = "Transportation inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the Transportation.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTransportation(int id, [FromBody] TransportationModel transportation)
        {
            if (transportation == null || id != transportation.TransportID)
                return BadRequest();

            var isUpdated = _transportationRepository.Update(transportation);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }
        [HttpGet("Bookings")]
        public IActionResult Getbookings()
        {
            var bookings = _transportationRepository.Getbookings();
            if (!bookings.Any())
                return NotFound("No bookings found.");

            return Ok(bookings);
        }
    }
}