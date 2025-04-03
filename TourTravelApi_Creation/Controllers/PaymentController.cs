using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentRepository _paymentRepository;
        public PaymentController(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = _paymentRepository.SelectAll();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var payment = _paymentRepository.SelectByPK(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var isDeleted = _paymentRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertPayment([FromBody] PaymentModel payment)
        {
            if (payment == null)
                return BadRequest();

            bool isInserted = _paymentRepository.Insert(payment);

            if (isInserted)
                return Ok(new { Message = "Payment inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the Payment.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] PaymentModel payment)
        {
            if (payment == null || id != payment.PaymentID)
                return BadRequest();

            var isUpdated = _paymentRepository.Update(payment);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        [HttpGet("Bookings")]
        public IActionResult Getbookings()
        {
            var bookings = _paymentRepository.Getbookings();
            if (!bookings.Any())
                return NotFound("No bookings found.");

            return Ok(bookings);
        }
    }
}