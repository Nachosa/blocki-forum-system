using ForumSystemDTO.UserDTO;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.Business.AuthenticationManager;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ForumSystem.Api.QueryParams;
using ForumSystemDTO.PostDTO;
using ForumSystem.Business;

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
        [HttpPost("")]
        public IActionResult CreateUser([FromBody] CreateUserDTO userDTO)
        {
            try
            {
                User mappedUser = mapper.Map<User>(userDTO);
                var createdUser = userService.CreateUser(mappedUser);
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
                authManager.AdminCheck(credentials);
                var usersDTO = userService.SearchBy(queryParams).Select(u => mapper.Map<GetUserDTO>(u));
                return Ok(usersDTO);

            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                return (StatusCode(StatusCodes.Status401Unauthorized, e.Message));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/posts")]
        public IActionResult GetUserPosts(int id, [FromHeader] string credentials, [FromQuery] PostQueryParameters queryParams)
        {
            try
            {
                authManager.UserCheck(credentials);
                var posts = userService.GetUserPosts(queryParams, id).Select(u => mapper.Map<GetPostDto>(u));
                return Ok(posts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("")]
        public IActionResult UpdateUser([FromHeader] string credentials, [FromBody] UpdateUserDTO userValues)
        {
            string[] usernameAndPassword = credentials.Split(':');
            string userName = usernameAndPassword[0];
            try
            {
                authManager.UserCheck(credentials);
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

        [HttpDelete("")]
        public IActionResult DeleteUser([FromHeader] string credentials, [FromQuery] string userName, int? id)
        {
            try
            {
                authManager.AdminCheck(credentials);
                userService.DeleteUser(userName, id);
                return Ok("User Deleted!");

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


    }
}
