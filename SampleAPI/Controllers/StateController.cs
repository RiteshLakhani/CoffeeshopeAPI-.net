using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Model;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _stateRepository;

        public StateController(StateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        [HttpGet]
        [Route("GetAllStates")]
        public IActionResult GetAllStates()
        {
            var states = _stateRepository.GetStates();
            return Ok(states);
        }

        [HttpGet("{id}")]
        public IActionResult GetStateById(int id)
        {
            var state = _stateRepository.SelectByPk(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var isDelete = _stateRepository.Delete(id);
            if (!isDelete)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertState([FromBody] StateModel state)
        {
            if (state == null)
            {
                return BadRequest("Invalid state data");
            }

            var isInserted = _stateRepository.Insert(state);
            if (isInserted)
            {
                return Ok(new { Message = "State inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the state");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, [FromBody] StateModel state)
        {
            if (state == null || id != state.StateID)
            {
                return BadRequest("Invalid state data or ID mismatch");
            }

            var isUpdated = _stateRepository.Update(state);
            if (!isUpdated)
            {
                return NotFound("State not found");
            }

            return NoContent();
        }

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = _stateRepository.GetCountries();
            if (!countries.Any())
            {
                return NotFound("No countries found");
            }
            return Ok(countries);
        }


    }
}
