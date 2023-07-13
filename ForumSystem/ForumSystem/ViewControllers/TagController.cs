using AutoMapper;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.TagService;
using ForumSystem.Business.UserService;
using ForumSystem.Business;
using ForumSystem.Web.Helpers.Contracts;
using Microsoft.AspNetCore.Mvc;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.Business.CommentService;
using ForumSystem.Web.Helpers;
using ForumSystemDTO.ViewModels.CommentViewModels;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace ForumSystem.Web.ViewControllers
{
	public class TagController : Controller
	{
		private readonly IAuthorizator authorizator;
		private readonly IPostService postService;
		private readonly ITagService tagService;
		private readonly IUserService userService;

		public TagController(IAuthorizator authorizator, IPostService postService, ITagService tagService, IUserService userService)
		{
			this.authorizator = authorizator;
			this.postService = postService;
			this.tagService = tagService;
			this.userService = userService;
		}

		[HttpGet]
		public IActionResult TagDetails(string tagName, int postId)
		{

			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}

				var tag = tagService.GetTagByName(tagName);

				ViewData["PostId"] = postId;

				return View(tag);
			}
			catch (EntityNotFoundException e)
			{
				Response.StatusCode = StatusCodes.Status404NotFound;
				ViewData["ErrorMessage"] = e.Message;

				return View("Error");
			}
		}

		[HttpGet]
		public IActionResult EditTag(int id, [FromQuery] int postId)
		{

			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}
				//var post = postService.GetPostById(postId);
				var tag = tagService.GetTagById(id);

				// tag should have author creator property
				// we should check if the currently-logged user is admin or the creator of the tag
				// only they can edit it
				// right now anyone can, as long as they're an admin or it's their post
				if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", tag.UserId))
				{
					HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
					ViewData["ErrorMessage"] = Authorizator.notAthorized;

					return View("Error");
				}

				if (!authorizator.isAdmin("roleId") && authorizator.isBlocked("roleId"))
				{
					throw new UnauthorizedAccessException("You'rе blocked - you can't perform this action.");
				}
                ViewData["PostId"] = postId;
                return View(tag);
			}
			catch (EntityNotFoundException e)
			{
				Response.StatusCode = StatusCodes.Status404NotFound;
				ViewData["ErrorMessage"] = e.Message;

				return View("Error");
			}
			catch (UnauthorizedAccessException e)
			{
				Response.StatusCode = StatusCodes.Status403Forbidden;
				ViewData["ErrorMessage"] = e.Message;

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
		public IActionResult UpdateTag(Tag tag,[FromQuery] int postId)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}
                var originalTag = tagService.GetTagById(tag.Id);
                if (ModelState.IsValid)
				{
                    if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", originalTag.UserId))
                    {
                        HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        ViewData["ErrorMessage"] = Authorizator.notAthorized;

                        return View("Error");
                    }

                    var updatedTag=tagService.UpdateTagName(tag.Id, tag);
                    return RedirectToAction("TagDetails", new { tagName = tag.Name, postId=postId });
				}

				return View(tag);
			}
			catch (EntityNotFoundException e)
			{
				Response.StatusCode = StatusCodes.Status404NotFound;
				ViewData["ErrorMessage"] = e.Message;

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
		public IActionResult DeleteTag(int id, int postId)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}

				//var post = postService.GetPostById(postId);
				var tag = tagService.GetTagById(id);

				ViewData["PostId"] = postId;

				if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", tag.UserId))
				{
					HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
					ViewData["ErrorMessage"] = Authorizator.notAthorized;

					return View("Error");
				}

				if (!authorizator.isAdmin("roleId") && authorizator.isBlocked("roleId"))
				{
					throw new UnauthorizedAccessException("You'rе blocked - you can't perform this action.");
				}

				return View(tag);
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
				this.Response.StatusCode = StatusCodes.Status500InternalServerError;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}
		}

		[HttpPost]
		public IActionResult DeleteTagConfirmed(int id, int postId)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}

				var post = postService.GetPostById(postId);
				var tag = tagService.GetTagById(id);

				if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", tag.UserId))
				{
					HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
					ViewData["ErrorMessage"] = Authorizator.notAthorized;

					return View("Error");
				}

				if (!authorizator.isAdmin("roleId") && authorizator.isBlocked("roleId"))
				{
					throw new UnauthorizedAccessException("You'rе blocked - you can't perform this action.");
				}

				tagService.DeleteTagById(id);

				return RedirectToAction("PostDetails", "Post", new { id = post.Id });
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
				this.Response.StatusCode = StatusCodes.Status500InternalServerError;
				this.ViewData["ErrorMessage"] = e.Message;
				return View("Error");
			}
		}

		[HttpPost]
		public IActionResult RemoveTag(int id, int postId)
		{
			try
			{
				if (!authorizator.isLogged("LoggedUser"))
				{
					return RedirectToAction("Login", "User");
				}

				var post = postService.GetPostById(postId);
				var tag = tagService.GetTagById(id);

				if (!authorizator.isAdmin("roleId") && !authorizator.isContentCreator("userId", post.UserId))
				{
					HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
					ViewData["ErrorMessage"] = Authorizator.notAthorized;

					return View("Error");
				}

				if (post.Tags.Any(pt => pt.Tag.Id == tag.Id))
				{
					var postTagToRemove = post.Tags.First(pt => pt.Tag.Id == tag.Id);

					// Remove the postTagToRemove from the post's tags collection
					post.Tags.Remove(postTagToRemove);

					postService.UpdatePostTags(post, HttpContext.Session.GetString("LoggedUser"));
				}

				return RedirectToAction("PostDetails", "Post", new { id = post.Id });
			}
			catch (EntityNotFoundException ex)
			{
				Response.StatusCode = StatusCodes.Status404NotFound;
				ViewData["ErrorMessage"] = ex.Message;
				return View("Error");
			}
			catch (UnauthenticatedOperationException e)
			{
				Response.StatusCode = StatusCodes.Status403Forbidden;
				ViewData["ErrorMessage"] = e.Message;
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