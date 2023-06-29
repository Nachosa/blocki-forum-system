using AutoMapper;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.UserRepo;
using ForumSystem.Web.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ForumSystem.Web.ViewControllers
{
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        public UserController(IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var registerUser = new RegisterUser();
            return View(registerUser);
        }

        [HttpPost]
        public IActionResult Register(RegisterUser registerUserFilled)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return View(registerUserFilled);
                }
                var user = new User();
                user.FirstName = registerUserFilled.FirstName;
                user.LastName = registerUserFilled.LastName;
                user.Username = registerUserFilled.Username;
                user.Email = registerUserFilled.Email;
                user.Password = registerUserFilled.Password;
                user.PhoneNumber = registerUserFilled.PhoneNumber;

                userService.CreateUser(user);
                return RedirectToAction("RegisteredSuccessful", "User");

            }
            catch (EmailAlreadyExistException e)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult RegisteredSuccessful()
        {
            return View();
        }

    }
}
