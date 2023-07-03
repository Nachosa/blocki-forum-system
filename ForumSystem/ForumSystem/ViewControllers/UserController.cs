using AutoMapper;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.UserRepo;
using ForumSystemDTO.ViewModels.UserViewModels;
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
		public UserController(IMapper mapper, IUserService userService, IAuthManager authManager)
		{
			this.mapper = mapper;
			this.userService = userService;
			this.authManager = authManager;
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
			return RedirectToAction("Index", "Home");
		}
		[HttpPost]
		public IActionResult Login(Login filledLoginForm)
		{
			if (!this.ModelState.IsValid)
			{
				return View(filledLoginForm);
			}

			try
			{
				var user = authManager.UserCheck(filledLoginForm.Username, filledLoginForm.Password);
				this.ViewData["userId"]= user.Id;
				this.HttpContext.Session.SetString("LoggedUser", filledLoginForm.Username);
				return RedirectToAction("Index", "Home");	
			}
			catch (EntityNotFoundException e)
			{
				this.Response.StatusCode = StatusCodes.Status404NotFound;
				this.ViewData["ErrorMessage"] = e.Message;
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
		public IActionResult UserDetails([FromRoute] int id)
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
