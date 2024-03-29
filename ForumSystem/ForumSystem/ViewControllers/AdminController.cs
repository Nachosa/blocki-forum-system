﻿using AutoMapper;
using ForumSystem.Business.AdminService;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.Web.Helpers;
using ForumSystem.Web.Helpers.Contracts;
using ForumSystemDTO.ViewModels.AdminModels;

using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ForumSystem.Web.ViewControllers
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IAdminService adminService;
        private readonly IAuthorizator authorizator;
        private readonly IMapper mapper;

        public AdminController(IUserService userService,IAdminService adminService,IAuthorizator authorizator,IMapper mapper)
        {
            this.userService = userService;
            this.adminService = adminService;
            this.authorizator = authorizator;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            if (!authorizator.isLogged("LoggedUser"))
            {
                return RedirectToAction("Login", "User");
            }
            if (!authorizator.isAdmin("roleId"))
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                return View("Error");
            }
            return View("SearchUser",new SearchUser());
        }

        [HttpGet]
        public IActionResult SearchUser()
        {
            if (!authorizator.isLogged("LoggedUser"))
            {
                return RedirectToAction("Login", "User");
            }
            if (!authorizator.isAdmin("roleId"))
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                return View("Error");
            }
            return View(new SearchUser());
        }

        [HttpPost]
        public IActionResult SearchUser(SearchUser filledForm)
        {
            if (!authorizator.isLogged("LoggedUser"))
            {
                return RedirectToAction("Login", "User");
            }
            if (!authorizator.isAdmin("roleId"))
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                return View("Error");
            }
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return View(filledForm);
                }
                var parameters = new UserQueryParams();
                var result = new List<User>();
                mapper.Map(filledForm, parameters);
                result = userService.SearchBy(parameters);
                filledForm.Users = result;
                return View(filledForm);
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

        [HttpGet] 
        public IActionResult BlockUser ([FromRoute] int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                if (!authorizator.isAdmin("roleId"))
                {
                    this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                    return View("Error");
                }
                _ = userService.GetUserById(id);
                this.ViewBag.userIdToBlock=id;
                return View();
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View();
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Block([FromRoute] int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                if (!authorizator.isAdmin("roleId"))
                {
                    this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                    return View("Error");
                }

                adminService.BlockUser(id, null);
                return View("BlockedSuccessful");
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View();
            }
            catch (EntityAlreadyBlockedException e)
            {
                this.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("BlockUser");
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
        }
        [HttpGet]
        public IActionResult UnBlockUser([FromRoute] int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                if (!authorizator.isAdmin("roleId"))
                {
                    this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                    return View("Error");
                }
                _ = userService.GetUserById(id);
                this.ViewBag.userIdToUnBlock = id;
                return View();
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View();
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult UnBlock([FromRoute] int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                if (!authorizator.isAdmin("roleId"))
                {
                    this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                    return View("Error");
                }

                adminService.UnBlockUser(id, null);
                return View("UnBlockedSuccessful");
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View();
            }
            catch (EntityAlreadyUnBlockedException e)
            {
                this.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("UnBlockUser");
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult MakeAdmin([FromRoute] int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                if (!authorizator.isAdmin("roleId"))
                {
                    this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                    return View("Error");
                }
                _ = userService.GetUserById(id);
                this.ViewBag.userIdMakeAdmin = id;
                return View();
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View();
            }
            catch (Exception e)
            {
                this.Response.StatusCode = StatusCodes.Status500InternalServerError;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult MakeUserAdmin([FromRoute] int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }
                if (!authorizator.isAdmin("roleId"))
                {
                    this.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = Authorizator.notAthorized;
                    return View("Error");
                }

                adminService.MakeUserAdmin(id, null);
                return View("MadeAdminSuccessful");
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return View();
            }
            catch (EntityAlreadyAdminException e)
            {
                this.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                this.ViewData["ErrorMessage"] = e.Message;
                return View("MakeAdmin");
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
