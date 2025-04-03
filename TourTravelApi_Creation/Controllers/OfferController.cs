using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly OfferRepository _offerRepository;

        public OfferController(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        [HttpGet]
        public IActionResult GetAllOffers()
        {
            var offers = _offerRepository.SelectAll();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public IActionResult GetOfferById(int id)
        {
            var offer = _offerRepository.SelectByPK(id);
            if (offer == null)
            {
                return NotFound();
            }
            return Ok(offer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOffer(int id)
        {
            var isDeleted = _offerRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertOffer([FromBody] OfferModel offer)
        {
            if (offer == null)
                return BadRequest();

            bool isInserted = _offerRepository.Insert(offer);

            if (isInserted)
                return Ok(new { Message = "Offer inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the Offer.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOffer(int id, [FromBody] OfferModel offer)
        {
            if (offer == null || id != offer.OfferID)
                return BadRequest();

            var isUpdated = _offerRepository.Update(offer);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }
    }
}
