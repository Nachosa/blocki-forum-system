using ForumSystem.Business;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.CommentService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Models;
using ForumSystemDTO.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Mvc;
using ForumSystemDTO.ViewModels.PostViewModels;
using ForumSystem.Web.Helpers.Contracts;
using Microsoft.AspNetCore.Http.Metadata;
using ForumSystem.Web.Helpers;
using Microsoft.Extensions.Hosting;

namespace ForumSystem.Web.ViewControllers
{
    public class CommentController : Controller
    {
        private readonly IAuthManager authManager;
        private readonly ICommentService commentService;
        private readonly IUserService userService;
        private readonly IAuthorizator authorizator;

        public CommentController(IAuthManager authManager, ICommentService commentService, IUserService userService, IAuthorizator authorizator)
        {
            this.authManager = authManager;
            this.commentService = commentService;
            this.userService = userService;
            this.authorizator = authorizator;
        }

        public IActionResult AddComment(int id)
        {
            try
            {
                int userId = HttpContext.Session.GetInt32("userId") ?? 0;
                var user = userService.GetUserById(userId);

                if (authManager.AdminCheck(user) == true || authManager.BlockedCheck(user) == false)
                {
                    return RedirectToAction("CommentForm", new { id });
                }

                throw new UnauthorizedAccessException("You are blocked and cannot perform this action.");
            }
            catch (EntityNotFoundException ex)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (UnauthorizedAccessException ex)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                ViewData["ErrorMessage"] = e.Message;

                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult CommentForm(int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }

                if (!authorizator.isAdmin("roleId") && authorizator.isBlocked("roleId"))
                {
                    throw new UnauthorizedAccessException("You are blocked and cannot perform this action.");
                }

                var model = new CommentFormViewModel
                {
                    PostId = id
                };

                return View(model);
            }
            catch (EntityNotFoundException ex)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (UnauthorizedAccessException ex)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                ViewData["ErrorMessage"] = e.Message;

                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult SubmitComment(CommentFormViewModel model)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }

                if (!authorizator.isAdmin("roleId") && authorizator.isBlocked("roleId"))
                {
                    throw new UnauthorizedAccessException("You are blocked and cannot perform this action.");
                }

                if (!ModelState.IsValid)
                {
                    return View("CommentForm", model);
                }

                var comment = new Comment
                {
                    Content = model.Content,
                    PostId = model.PostId,
                    UserId = HttpContext.Session.GetInt32("userId") ?? 0
                };

                _ = commentService.CreateComment(comment, model.PostId);
                return RedirectToAction("PostDetails", "Post", new { id = model.PostId });
            }
            catch (EntityNotFoundException ex)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (UnauthorizedAccessException ex)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                ViewData["ErrorMessage"] = e.Message;

                return View("Error");
            }
        }

        public IActionResult EditComment(int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }

                var comment = commentService.GetCommentById(id);

                if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", comment.UserId))
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    ViewData["ErrorMessage"] = Authorizator.notAthorized;

                    return View("Error");
                }

                if (!authorizator.isAdmin("roleId") && authorizator.isBlocked("roleId"))
                {
                    throw new UnauthorizedAccessException("You are blocked and cannot perform this action.");
                }

                var model = new EditCommentViewModel
                {
                    CommentId = id,
                    EditedComment = comment.Content
                };

                return View("EditCommentForm", model);
            }
            catch (EntityNotFoundException ex)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (UnauthorizedAccessException ex)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                ViewData["ErrorMessage"] = e.Message;

                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult UpdateComment(EditCommentViewModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View("EditCommentForm", model);
                }

                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }

                var comment = commentService.GetCommentById(model.CommentId);

                if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", comment.UserId))
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    ViewData["ErrorMessage"] = Authorizator.notAthorized;

                    return View("Error");
                }

                if (!authorizator.isAdmin("roleId") && authorizator.isBlocked("roleId"))
                {
                    throw new UnauthorizedAccessException("You are blocked and cannot perform this action.");
                }

                comment.Content = model.EditedComment;
                commentService.UpdateComment(comment, model.CommentId);
                return RedirectToAction("PostDetails", "Post", new { id = comment.PostId });
            }
            catch (EntityNotFoundException ex)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (UnauthorizedAccessException ex)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                ViewData["ErrorMessage"] = e.Message;

                return View("Error");
            }
        }

        public IActionResult DeleteComment(int id)
        {
            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }

                var comment = commentService.GetCommentById(id);

                if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", comment.UserId))
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    ViewData["ErrorMessage"] = Authorizator.notAthorized;

                    return View("Error");
                }

                if (!authorizator.isAdmin("roleId") && authorizator.isBlocked("roleId"))
                {
                    throw new UnauthorizedAccessException("You are blocked and cannot perform this action.");
                }

                var model = new EditCommentViewModel
                {
                    CommentId = id,
                    EditedComment = comment.Content
                };

                return View("DeleteCommentForm", model);
            }
            catch (EntityNotFoundException ex)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (UnauthorizedAccessException ex)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                ViewData["ErrorMessage"] = e.Message;

                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Delete(EditCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("DeleteCommentForm", model);
            }

            try
            {
                if (!authorizator.isLogged("LoggedUser"))
                {
                    return RedirectToAction("Login", "User");
                }

                var comment = commentService.GetCommentById(model.CommentId);

                if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", comment.UserId))
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    ViewData["ErrorMessage"] = Authorizator.notAthorized;

                    return View("Error");
                }

                if (!authorizator.isAdmin("roleId") && authorizator.isBlocked("roleId"))
                {
                    throw new UnauthorizedAccessException("You are blocked and cannot perform this action.");
                }

                commentService.DeleteCommentById(comment.Id, HttpContext.Session.GetString("LoggedUser"));
                return RedirectToAction("PostDetails", "Post", new { id = comment.PostId });
            }
            catch (EntityNotFoundException ex)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (UnauthorizedAccessException ex)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                ViewData["ErrorMessage"] = ex.Message;

                return View("Error");
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                ViewData["ErrorMessage"] = e.Message;

                return View("Error");
            }
        }

		[HttpGet]
		public IActionResult LikeComment(int id)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}

				if (authorizator.isBlocked("roleId"))
				{
					throw new UnauthorizedAccessException("You are blocked and cannot perform this action.");
				}

				string loggedUsername = HttpContext.Session.GetString("LoggedUser");

				_ = userService.GetUserByUserName(loggedUsername);

				commentService.LikeComment(id, loggedUsername);
                var comment = commentService.GetCommentById(id);

				return RedirectToAction("PostDetails", "Post", new { Id = comment.PostId });
			}
			catch (EntityNotFoundException e)
			{
				Response.StatusCode = StatusCodes.Status404NotFound;
				ViewData["ErrorMessage"] = e.Message;

				return View("Error");
			}
			catch (UnauthorizedAccessException ex)
			{
				Response.StatusCode = StatusCodes.Status403Forbidden;
				ViewData["ErrorMessage"] = ex.Message;

				return View("Error");
			}
			catch (Exception e)
			{
				Response.StatusCode = StatusCodes.Status500InternalServerError;
				ViewData["ErrorMessage"] = e.Message;

				return View("Error");
			}
		}

		[HttpGet]
		public IActionResult DislikeComment(int id)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}

				if (authorizator.isBlocked("roleId"))
				{
					throw new UnauthorizedAccessException("You are blocked and cannot perform this action.");
				}

				string loggedUsername = HttpContext.Session.GetString("LoggedUser");

				_ = userService.GetUserByUserName(loggedUsername);

				commentService.DislikeComment(id, loggedUsername);
                var comment = commentService.GetCommentById(id);

				return RedirectToAction("PostDetails", "Post", new { Id = comment.PostId });
			}
			catch (EntityNotFoundException e)
			{
				Response.StatusCode = StatusCodes.Status404NotFound;
				ViewData["ErrorMessage"] = e.Message;

				return View("Error");
			}
			catch (UnauthorizedAccessException ex)
			{
				Response.StatusCode = StatusCodes.Status403Forbidden;
				ViewData["ErrorMessage"] = ex.Message;

				return View("Error");
			}
			catch (Exception e)
			{
				Response.StatusCode = StatusCodes.Status500InternalServerError;
				ViewData["ErrorMessage"] = e.Message;

				return View("Error");
			}
		}
	}
}