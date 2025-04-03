using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerRepository.SelectAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _customerRepository.SelectByPK(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var isDeleted = _customerRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertCustomer([FromBody] CustomerModel customer)
        {
            if (customer == null)
                return BadRequest();

            bool isInserted = _customerRepository.Insert(customer);

            if (isInserted)
                return Ok(new { Message = "Customer inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the customer.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerModel customer)
        {
            if (customer == null || id != customer.CustomerID)
                return BadRequest();

            var isUpdated = _customerRepository.Update(customer);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }


        [HttpPost("SignUpcustomer")]
        public IActionResult SignUpcustomer([FromBody] CustomerModel customerModel)
        {
            try
            {
                if (customerModel == null)
                {
                    return BadRequest(new { Message = "Invalid customer data." });
                }

                string errorMessage;
                bool isInserted = _customerRepository.Signup(customerModel, out errorMessage);

                if (isInserted)
                {
                    Console.WriteLine("Signup successful!");
                    return Ok(new { Message = "Signup successful!" });
                }
                else
                {
                    Console.WriteLine($"Signup failed: {errorMessage}");
                    return StatusCode(500, new { Message = "Signup failed.", Error = errorMessage });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred during signup.", Error = ex.Message });
            }
        }


        [HttpPost("LoginCustomer")]
        public IActionResult LoginCustomer([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest(new { Message = "Invalid login data." });
            }

            string errorMessage;
            string token = _customerRepository.Login(loginModel, out errorMessage);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = errorMessage });
            }

            return Ok(new
            {
                Token = token,
                Message = "Login successfully!"
            });
        }
    }

    }  //[HttpPost("SignUpcustomer")]
   //public IActionResult SignUpcustomer([FromBody] CustomerModel customerModel)
   //{
   //    if (customerModel == null)
   //    {
   //        return BadRequest(new { Message = "Invalid customer data." });
   //    }

//    string errorMessage;
//    bool isInserted = _customerRepository.Signup(customerModel, out errorMessage);

//    if (!isInserted)
//    {
//        return BadRequest(new { Message = errorMessage });
//    }

//    return Ok(new { Message = "customer registered successfully!" });
//}