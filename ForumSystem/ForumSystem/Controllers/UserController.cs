using ForumSystem.Business.CreateAndUpdate_UserDTO;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("")]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(userService.GetAllUsers());
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        //Get user or users by FirstName
        [HttpGet("name")]
        public IActionResult GetUsersByFirstName([FromQuery] string firstname)
        {
            try
            {
                var usersWithThatName=userService.GetUsersByFirstName(firstname);
                return Ok(usersWithThatName);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("email")]
        public IActionResult GetUsersByEmail([FromQuery] string email)
        {
            try
            {
                var usersWithThatEmail = userService.GetUserByEmail(email);
                return Ok(usersWithThatEmail);
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("username")]
        public IActionResult GetUsersByUserName([FromQuery] string userName)
        {
            try
            {
                var usersWithThatEmail = userService.GetUserByUserName(userName);
                return Ok(usersWithThatEmail);
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        //Get user by ID
        [HttpGet("{Id:int}")]
        public IActionResult GetUserById(int Id)
        {
            try
            {
                var user = userService.GetUserById(Id);
                return Ok(user);

            }
            catch (EntityNotFoundException e)
            {

                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreateUser([FromBody] CreateUserDTO user)
        {
            var createdUser = userService.CreateUser(user);
            return Ok(createdUser);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateUser(int Id, [FromBody] UpdateUserDTO userValues)
        {
            try
            {
                var updatedUser = userService.UpdateUser(Id, userValues);
                return Ok("Update successful!");

            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            try
            {
                userService.DeleteUser(Id);
                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
