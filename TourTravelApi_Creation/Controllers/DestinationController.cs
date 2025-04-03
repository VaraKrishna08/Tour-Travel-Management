using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private readonly DestinationRepository _destinationRepository;

        public DestinationController(DestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        [HttpGet]
        public IActionResult GetAllDestinations()
        {
            var destinations = _destinationRepository.SelectAll();
            return Ok(destinations);
        }

        [HttpGet("{id}")]
        public IActionResult GetDestinationById(int id)
        {
            var destination = _destinationRepository.SelectByPK(id);
            if (destination == null)
            {
                return NotFound();
            }
            return Ok(destination);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDestination(int id)
        {
            var isDeleted = _destinationRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult Insertdestination([FromBody] DestinationModel destination)
        {
            if (destination == null)
                return BadRequest();

            bool isInserted = _destinationRepository.Insert(destination);

            if (isInserted)
                return Ok(new { Message = "destination inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the destination.");
        }

        [HttpPut("{id}")]
        public IActionResult Updatedestination(int id, [FromBody] DestinationModel destination)
        {
            if (destination == null || id != destination.DestinationID)
                return BadRequest();

            var isUpdated = _destinationRepository.Update(destination);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }
    }
}
