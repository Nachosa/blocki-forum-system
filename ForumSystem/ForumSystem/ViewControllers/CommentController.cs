using ForumSystem.Business;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.CommentService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Models;
using ForumSystemDTO.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Mvc;
using ForumSystemDTO.ViewModels.PostViewModels;

namespace ForumSystem.Web.ViewControllers
{
    public class CommentController : Controller
	{
		private readonly IAuthManager authManager;
		private readonly ICommentService commentService;
		private readonly IUserService userService;

		public CommentController(IAuthManager authManager, ICommentService commentService, IUserService userService)
		{
			this.authManager = authManager;
			this.commentService = commentService;
			this.userService = userService;
		}

		public IActionResult AddComment(int id)
		{
			try
			{
                int userId = HttpContext.Session.GetInt32("userId") ?? 0;
                var user = userService.GetUserById(userId);

				if (user == null || userId == 0)
				{
					throw new EntityNotFoundException("Entity not found.");
				}

				if (authManager.AdminCheck(user) == true || authManager.BlockedCheck(user) == false)
				{
                    return RedirectToAction("CommentForm", new { id });
                }

                throw new UnauthorizedAccessException("You'rе blocked - you can't perform this action.");
            }
			//TODO:Тук липсва някакъв ексепшън, трябва да проверя по-късно.
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
        }

		[HttpGet]
		public IActionResult CommentForm(int id)
		{
			var model = new CommentFormViewModel
			{
				PostId = id
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult SubmitComment(CommentFormViewModel model)
        {
			if (!ModelState.IsValid)
			{
                return View("CommentForm", model);
            }

            var comment = new Comment
            {
                Content = model.CommentContent,
                PostId = model.PostId,
                UserId = HttpContext.Session.GetInt32("userId") ?? 0
            };

            commentService.CreateComment(comment, model.PostId);
            return RedirectToAction("PostDetails", "Post", new { id = model.PostId });
        }

		public IActionResult EditComment(int id)
		{
			if (!IsLogged("LoggedUser"))
			{
				return RedirectToAction("Login", "User");
			}

			var comment = commentService.GetCommentById(id);

			if (!IsAdmin("roleId") && HttpContext.Session.GetInt32("userId") != comment.UserId)
			{
				HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
				ViewData["ErrorMessage"] = "You're not the author of this comment!";

				return View("Error");
			}

			var model = new EditCommentViewModel
			{
				CommentId = id,
				EditedComment = comment.Content
			};

			return View("EditCommentForm", model);
		}

		public IActionResult DeleteComment(int id)
		{
			if (!IsLogged("LoggedUser"))
			{
				return RedirectToAction("Login", "User");
			}

			var comment = commentService.GetCommentById(id);

			if (!IsAdmin("roleId") && HttpContext.Session.GetInt32("userId") != comment.UserId)
			{
				HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
				ViewData["ErrorMessage"] = "You're not the author of this comment!";

				return View("Error");
			}

			var model = new EditCommentViewModel
			{
				CommentId = id,
				EditedComment = comment.Content
			};

			return View("DeleteCommentForm", model);
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
				int roleId = (int) HttpContext.Session.GetInt32("roleId");

				if (authManager.BlockedCheck(roleId))
				{
					throw new UnauthorizedAccessException("You're blocked. You can't perform this action.");
				}

				var comment = commentService.GetCommentById(model.CommentId);

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
		}

		[HttpPost]
        public IActionResult UpdateComment(EditCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCommentForm", model);
            }

            try
            {
				int roleId = (int) HttpContext.Session.GetInt32("roleId");

				if (authManager.BlockedCheck(roleId))
				{
					throw new UnauthorizedAccessException("You're blocked. You can't perform this action.");
				}

				var comment = commentService.GetCommentById(model.CommentId);

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
        }

		private bool IsAdmin(string key)
		{
			if (HttpContext.Session.GetInt32(key) != 3)
			{
				return false;
			}

			return true;
		}

		private bool IsLogged(string key)
		{
			if (!HttpContext.Session.Keys.Contains(key))
			{
				return false;
			}

			return true;
		}
	}
}