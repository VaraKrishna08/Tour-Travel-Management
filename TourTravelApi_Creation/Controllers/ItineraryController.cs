using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItineraryController : ControllerBase
    {
        private readonly ItineraryRepository _itineraryRepository;

        public ItineraryController(ItineraryRepository itineraryRepository)
        {
            _itineraryRepository = itineraryRepository;
        }

        [HttpGet]
        public IActionResult GetAllItinerary()
        {
            var itinerary = _itineraryRepository.SelectAll();
            return Ok(itinerary);
        }

        [HttpGet("{id}")]
        public IActionResult GetItineraryById(int id)
        {
            var itinerary = _itineraryRepository.SelectByPK(id);
            if (itinerary == null)
            {
                return NotFound();
            }
            return Ok(itinerary);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItinerary(int id)
        {
            var isDeleted = _itineraryRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult Insertitinerary([FromBody] ItineraryModel itinerary)
        {
            if (itinerary == null)
                return BadRequest();

            bool isInserted = _itineraryRepository.Insert(itinerary);

            if (isInserted)
                return Ok(new { Message = "itinerary inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the itinerary.");
        }

        [HttpPut("{id}")]
        public IActionResult Updateitinerary(int id, [FromBody] ItineraryModel itinerary)
        {
            if (itinerary == null || id != itinerary.ItineraryID)
                return BadRequest();

            var isUpdated = _itineraryRepository.Update(itinerary);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }


        [HttpGet("Packages")]
        public IActionResult Getpackages()
        {
            var packages = _itineraryRepository.Getpackages();
            if (!packages.Any())
                return NotFound("No packages found.");

            return Ok(packages);
        }
    }
}
