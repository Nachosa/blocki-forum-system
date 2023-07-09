using ForumSystem.Business.AdminService;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForumSystem.Web.ApiControllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IAuthManager authManager;
        private readonly IAdminService adminService;
        public AdminApiController(IAuthManager authManager, IAdminService adminService)
        {
            this.authManager = authManager;
            this.adminService = adminService;
        }


        [HttpPut("makeadmin")]
        public IActionResult MakeUserAdmin([FromHeader] string credentials, [FromQuery] int? id, string email)
        {
            try
            {
                authManager.AdminCheck(credentials);
                adminService.MakeUserAdmin(id, email);
                return Ok("User promoted to admin successfully!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("block")]
        public IActionResult BlockUser([FromHeader] string credentials, [FromQuery] int? id, string email)
        {
            try
            {
                authManager.AdminCheck(credentials);
                adminService.BlockUser(id, email);
                return Ok("User blocked successfully!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("unblock")]
        public IActionResult UnblockUser([FromHeader] string credentials, [FromQuery] int? id, string email)
        {
            try
            {
                authManager.AdminCheck(credentials);
                adminService.UnBlockUser(id, email);
                return Ok("User unblocked successfully!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{postId}")]
        public IActionResult DeletePost([FromHeader] string credentials, int postId)
        {
            try
            {
                authManager.AdminCheck(credentials);
                adminService.DeletePost(postId);
                return Ok("Post Deleted!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
