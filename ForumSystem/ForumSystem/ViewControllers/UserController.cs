using AutoMapper;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.UserRepo;
using ForumSystem.Web.Helpers;
using ForumSystem.Web.Helpers.Contracts;
using ForumSystemDTO.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace ForumSystem.Web.ViewControllers
{
    public class UserController : Controller
    {

		private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IAuthManager authManager;
        private readonly IAuthorizator authorizator;
        public UserController(IMapper mapper, IUserService userService, IAuthManager authManager, IAuthorizator authenticator)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.authManager = authManager;
            this.authorizator = authenticator;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var login = new Login();
            return View(login);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove("LoggedUser");
            this.HttpContext.Session.Remove("userId");
            this.HttpContext.Session.Remove("roleId");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Login(Login filledLoginForm)
        {
            //Влизаме тук ако имаме Null Username or Password!
            if (!this.ModelState.IsValid)
            {
                return View(filledLoginForm);
            }

            try
            {
                var user = authManager.UserCheck(filledLoginForm.Username, filledLoginForm.Password);
                this.HttpContext.Session.SetString("LoggedUser", filledLoginForm.Username);
                this.HttpContext.Session.SetInt32("userId", user.Id);
                this.HttpContext.Session.SetInt32("roleId", user.RoleId);
                return RedirectToAction("Index", "Home");
            }
            catch (EntityNotFoundException)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = "Invalid username or password!";
                return View(filledLoginForm);
            }
            catch (UnauthenticatedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = e.Message;
                return View(filledLoginForm);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }

        }

        [HttpGet]
        public IActionResult UserDetails(int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
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
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId",id))
                {
                    this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                    return View("Error");
                }
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
                return View("EditSuccessful");

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

                var user = mapper.Map<User>(registerUserFilled);
                userService.CreateUser(user);
                return View("RegisteredSuccessful");

            }
            catch (EmailAlreadyExistException e)
            {
                this.Response.StatusCode = StatusCodes.Status409Conflict;
                this.ViewData["ErrorMessage"] = e.Message;
                return View(registerUserFilled);
                
            }
            catch (UsernameAlreadyExistException e)
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
        public IActionResult DeleteUser(int id)
        {

            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", id))
                {
                    this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                    return View("Error");
                }
                _ = userService.GetUserById(id);
                this.ViewBag.userIdToDelete = id;
                return View();

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
        [HttpPost, ActionName("DeleteUser")]
        public IActionResult Delete(int id)
        {
            try
            {
                userService.DeleteUser(null,id);

                if (!authorizator.isAdmin("roleId"))
                {
                    return RedirectToAction("Logout", "User");
                }
                else
                {            
                    return View("DeletedSuccessful");
                }

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

    }
}
