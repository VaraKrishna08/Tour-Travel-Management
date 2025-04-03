using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;
namespace TourTravelApi_Creation.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employeess = _employeeRepository.SelectAll();
            return Ok(employeess);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employees = _employeeRepository.SelectByPK(id);
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var isDeleted = _employeeRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertEmployee([FromBody] EmployeeModel employees)
        {
            if (employees == null)
                return BadRequest();

            bool isInserted = _employeeRepository.Insert(employees);

            if (isInserted)
                return Ok(new { Message = "employees inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the employees.");
        }

        [HttpPut("{id}")]
        public IActionResult Updateemployees(int id, [FromBody] EmployeeModel employees)
        {
            if (employees == null || id != employees.EmployeeID)
                return BadRequest();

            var isUpdated = _employeeRepository.Update(employees);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }
    }
}