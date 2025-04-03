using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourTravelApi_Creation.Data;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly PackageRepository _packageRepository;

        public PackageController(PackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        [HttpGet]
        public IActionResult GetAllPackages()
        {
            var packages = _packageRepository.SelectAll();
            return Ok(packages);
        }

        [HttpGet("{id}")]
        public IActionResult GetPackageById(int id)
        {
            var package = _packageRepository.SelectByPK(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePackage(int id)
        {
            var isDeleted = _packageRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertPackage([FromBody] PackageModel package)
        {
            if (package == null)
                return BadRequest();

            bool isInserted = _packageRepository.Insert(package);

            if (isInserted)
                return Ok(new { Message = "Package inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the Package.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePackage(int id, [FromBody] PackageModel package)
        {
            if (package == null || id != package.PackageID)
                return BadRequest();

            var isUpdated = _packageRepository.Update(package);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        [HttpGet("Destinations")]
        public IActionResult Getdestinations()
        {
            var destinations = _packageRepository.Getdestinations();
            if (!destinations.Any())
                return NotFound("No destinations found.");

            return Ok(destinations);
        }
    }

}
