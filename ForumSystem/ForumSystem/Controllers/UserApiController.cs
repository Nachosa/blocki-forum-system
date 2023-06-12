using ForumSystemDTO.UserDTO;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.Business.AuthenticationManager;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthManager authManager;

        public UserApiController(IUserService userService,IAuthManager authManager)
        {
            this.userService = userService;
            this.authManager = authManager;
        }
        //Get all users or get Users by First name ,Email and Username
        [HttpGet("")]
        public IActionResult GetUsers([FromQuery] UserQueryParams queryParams)
        {
            try
            {
                if (queryParams.UserName is null &
                    queryParams.FirstName is null &
                    queryParams.Email is null)
                {
                    return Ok(userService.GetAllUsers());
                }
                else
                {
                    return Ok(userService.SearchBy(queryParams));
                }
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

        [HttpPut("update")]
        public IActionResult UpdateUser([FromHeader] string credentials,[FromBody] UpdateUserDTO userValues)
        {
            string[] usernameAndPassword = credentials.Split(':');
            string userName = usernameAndPassword[0];
            try
            {
                authManager.CheckUser(credentials);
                var updatedUser = userService.UpdateUser(userName, userValues);
                return Ok(updatedUser);

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
                return Ok("User Deleted!");

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
