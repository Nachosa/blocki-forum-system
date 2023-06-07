using ForumSystem.Business.CreateAndUpdate_UserDTO;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
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
            catch (Exception e)
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
                var user = userService.FindUserById(Id);
                return Ok(user);

            }
            catch (Exception e)
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
        public IActionResult UpdateUser(int Id, [FromBody] User userValues)
        {
            var updatedUser = userService.UpdateUser(Id, userValues);
            return Ok(updatedUser);
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
