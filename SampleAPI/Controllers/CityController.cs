using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using SampleAPI.Model;
using SampleAPI.Data;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository _cityRepository;

        public CityController(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet("GetAllCities")]
        public IActionResult GetAllCities()
        {
            var city = _cityRepository.GetCity();
            return Ok(city);
        }

        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = _cityRepository.SelectByPk(id);
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var isDelete = _cityRepository.Delete(id);
            if(!isDelete)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertCity([FromBody]CityModel city)
        {
            if(city == null)
                return BadRequest();

            bool isInserted = _cityRepository.Insert(city);

            if(isInserted)
            {
                return Ok(new { Message = "City inserted successfully!" });
            }

            return StatusCode(500, "An error occurred while inserting the city");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, [FromBody] CityModel city)
        {
            if(city == null || id!=city.CityID)
            {
                return BadRequest();
            }

            var isUpdated = _cityRepository.Update(city);
            if(!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = _cityRepository.GetCountries();
            if(!countries.Any())
            {
                return NotFound("No countries found");
            }
            return Ok(countries);
        }

        [HttpGet("states/{countryID}")]
        public IActionResult GetStatesByCountryID(int countryID)
        {
            if(countryID<=0)
            {
                return BadRequest("Invalid CountryID");
            }

            var states = _cityRepository.GetStateByCountryID(countryID);
            if(!states.Any())
            {
                return NotFound("No States found for the given CountryID");
            }

            return Ok(states);
        }
    }
}