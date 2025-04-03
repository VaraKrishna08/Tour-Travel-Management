using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackRepository _feedbackRepository;

        public FeedbackController(FeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        public IActionResult GetAllFeedbacks()
        {
            var feedbacks = _feedbackRepository.SelectAll();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public IActionResult GetFeedbackById(int id)
        {
            var feedback = _feedbackRepository.SelectByPK(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return Ok(feedback);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeedback(int id)
        {
            var isDeleted = _feedbackRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertFeedback([FromBody] FeedbackModel feedback)
        {
            if (feedback == null)
                return BadRequest();

            bool isInserted = _feedbackRepository.Insert(feedback);

            if (isInserted)
                return Ok(new { Message = "Feedback inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the feedback.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFeedback(int id, [FromBody] FeedbackModel feedback)
        {
            if (feedback == null || id != feedback.FeedbackID)
                return BadRequest();

            var isUpdated = _feedbackRepository.Update(feedback);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        [HttpGet("Customers")]
        public IActionResult Getcustomers()
        {
            var customers = _feedbackRepository.Getcustomers();
            if (!customers.Any())
                return NotFound("No customers found.");

            return Ok(customers);
        }

        [HttpGet("Packages")]
        public IActionResult Getpackages()
        {
            var packages = _feedbackRepository.Getpackages();
            if (!packages.Any())
                return NotFound("No packages found.");

            return Ok(packages);
        }
    }
}
