using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        //На този ROUTE се очаква да бъде Public частта!!!

        //The public part must be accessible without authentication.
        //On the home page, anonymous users must be presented with the core features of
        //the platform as well as how many people are using it and how many posts have
        //    been created so far.
        //Anonymous users must be able to register and log in. 
        //Anonymous users should be able to see a list of the top 10 most commented posts
        //and a list of the 10 most recently created posts.
    }
}
