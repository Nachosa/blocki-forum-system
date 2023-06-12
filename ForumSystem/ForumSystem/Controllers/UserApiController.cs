using ForumSystemDTO.UserDTO;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.Business.AuthenticationManager;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthManager authManager;
        private readonly IMapper mapper;

        public UserApiController(IUserService userService, IAuthManager authManager, IMapper mapper)
        {
            this.userService = userService;
            this.authManager = authManager;
            this.mapper = mapper;
        }
        //Get all users or get Users by First name ,Email and Username
        [HttpPost("")]
        public IActionResult CreateUser([FromBody] CreateUserDTO user)
        {
            try
            {
                var createdUser = userService.CreateUser(user);
                GetUserDTO result = mapper.Map<GetUserDTO>(createdUser);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("")]
        public IActionResult GetUsers([FromHeader] string credentials, [FromQuery] UserQueryParams queryParams)
        {
            try
            {
                if (queryParams.UserName is null &
                    queryParams.FirstName is null &
                    queryParams.Email is null)
                {
                    authManager.IsAdmin(credentials);
                    var usersDTO = userService.GetAllUsers().Select(u => mapper.Map<GetUserDTO>(u));
                    return Ok(usersDTO);
                }
                else
                {
                    authManager.IsAdmin(credentials);
                    var usersDTO = userService.GetAllUsers().Select(u => mapper.Map<GetUserDTO>(u));
                    return Ok(usersDTO);
                }
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        public IActionResult UpdateUser([FromHeader] string credentials, [FromBody] UpdateUserDTO userValues)
        {
            string[] usernameAndPassword = credentials.Split(':');
            string userName = usernameAndPassword[0];
            try
            {
                authManager.CheckUser(credentials);
                var mapped = mapper.Map<User>(userValues);
                var updatedUser = userService.UpdateUser(userName, mapped);
                GetUserDTO updatedUserDTO = mapper.Map<GetUserDTO>(updatedUser);

                return Ok(updatedUserDTO);

            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (EmailAlreadyExistException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }

        }

        [HttpDelete("delete")]
        public IActionResult DeleteUser([FromHeader] string credentials, [FromBody] string userName)
        {
            try
            {
                authManager.IsAdmin(credentials);
                userService.DeleteUser(userName);
                return Ok("User Deleted!");

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
