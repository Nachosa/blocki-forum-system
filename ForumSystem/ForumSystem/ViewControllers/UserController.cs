using AutoMapper;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.UserRepo;
using ForumSystemDTO.ViewModels.UserViewModels;
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
        public IActionResult UserDetails(int id)
        {
            try
            {
                var user = userService.GetUserById(id);
                return View(user);
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult EditUser([FromRoute] int id)
        {
            try
            {
                var user = userService.GetUserById(id);
                EditUser userForm = new EditUser();
                userForm.Id = user.Id;
                return View(userForm);

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult EditUser([FromRoute] int id, EditUser editedUser)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return View(editedUser);
                }
                if (editedUser.FirstName is null &&
                    editedUser.LastName is null &&
                    editedUser.Password is null &&
                    editedUser.Email is null &&
                    editedUser.PhoneNumber is null)
                {
                    this.ViewData["ErrorMessage"] = "There's nothing filled!";
                    return View(editedUser);
                }
                var mappedUser = mapper.Map<User>(editedUser);
                userService.UpdateUser(id, mappedUser);
                return RedirectToAction("EditSuccessful", "User");

            }
            catch (EmailAlreadyExistException e)
            {
                this.Response.StatusCode = StatusCodes.Status409Conflict;
                this.ViewData["ErrorMessage"] = e.Message;
                return View(editedUser);

            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }

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

                //Вече има автоматично мапване.
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
                this.Response.StatusCode = StatusCodes.Status409Conflict;
                this.ViewData["ErrorMessage"] = e.Message;
                return View(registerUserFilled);

            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult RegisteredSuccessful()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditSuccessful()
        {
            return View();
        }
    }
}
