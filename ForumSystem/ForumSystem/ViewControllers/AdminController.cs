﻿using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.QueryParams;
using ForumSystemDTO.ViewModels.AdminModels;

using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Web.ViewControllers
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;

        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchUser()
        {
            if (!isLogged("LoggedUser"))
            {
                return RedirectToAction("Login", "User");
            }
            if (!isAdmin("roleId"))
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = "You'r not admin!";
                return View("Error");
            }
            return View(new SearchUser());
        }


        [HttpPost]
        public IActionResult SearchUser(SearchUser filledForm)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return View(filledForm);
                }
                var parameters = new UserQueryParams();
                if (filledForm.SearchOption == "FirstName")
                {
                    parameters.FirstName = filledForm.SearchOptionValue;
                    var result = userService.SearchBy(parameters);
                }
                else if (filledForm.SearchOption == "Email")
                {
                    parameters.Email = filledForm.SearchOptionValue;
                    var result = userService.SearchBy(parameters);
                }
                else if (filledForm.SearchOption == "UserName")
                {
                    parameters.UserName = filledForm.SearchOptionValue;
                    var result = userService.SearchBy(parameters);
                }

                return View();
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View(filledForm);
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
        }

        private bool isLogged(string key)
        {
            if (!this.HttpContext.Session.Keys.Contains(key))
            {
                return false;
            }
            return true;
        }
        private bool isAdmin(string key)
        {
            if (this.HttpContext.Session.GetInt32(key) != 3)
            {
                return false;
            }
            return true;

        }
    }
}