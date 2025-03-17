using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Model;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Get All Users
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }
        #endregion

        #region Get User By ID
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.SelectByPk(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
        #endregion

        #region Insert User
        [HttpPost]
        public IActionResult InsertUser([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data");
            }

            var isInserted = _userRepository.Insert(user);
            if (isInserted)
            {
                return Ok(new { Message = "User inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the user");
        }
        #endregion

        #region Update User
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel user)
        {
            if (user == null || id != user.UserID)
            {
                return BadRequest("Invalid user data or ID mismatch");
            }

            var isUpdated = _userRepository.Update(user);
            if (!isUpdated)
            {
                return NotFound("User not found");
            }

            return NoContent();
        }
        #endregion

        #region Delete User
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var isDeleted = _userRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("User not found");
            }
            return NoContent();
        }
        #endregion
    }
}
