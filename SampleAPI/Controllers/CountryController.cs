using Microsoft.AspNetCore.Mvc;
using SampleAPI.Model;
using System.Data;
using SampleAPI.Data;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _countryRepository;

        public CountryController(CountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [Route("GetAllCountries")]
        public IActionResult GetAllCountry()
        {
            var country = _countryRepository.GetCountry();
            return Ok(country);
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var country = _countryRepository.SelectByPk(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var isDelete = _countryRepository.Delete(id);
            if (!isDelete)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertCountry([FromBody] CountryModel country)
        {
            if (country == null)
            {
                return BadRequest("Invalid country data");
            }

            var isInserted = _countryRepository.Insert(country);
            if (isInserted)
            {
                return Ok(new { Message = "Country inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the country");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] CountryModel country)
        {
            if (country == null || id != country.CountryID)
            {
                return BadRequest("Invalid country data or ID mismatch");
            }

            var isUpdated = _countryRepository.Update(country);
            if (!isUpdated)
            {
                return NotFound("Country not found");
            }

            return NoContent();
        }

    }
 }
