﻿using ForumSystem.Business;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Models;
using ForumSystemDTO.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Web.ViewControllers
{
    public class CommentController : Controller
	{
		private readonly ICommentService commentService;

		public CommentController(ICommentService commentService)
		{
			this.commentService = commentService;
		}

		public IActionResult AddComment(int postId)
		{
			return RedirectToAction("CommentForm", "Comment", new { postId });
		}

		// responsible for rendering the comment form view
		[HttpGet]
		public IActionResult CommentForm(int postId)
		{
			var model = new CommentFormViewModel
			{
				PostId = postId
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult SubmitComment(CommentFormViewModel model)
		{
			if (ModelState.IsValid)
			{
				var comment = new Comment
				{
					Content = model.CommentContent,
					PostId = model.PostId,
					UserId = 2 // TODO: take current signed-in user's id
				};

				commentService.CreateComment(comment, model.PostId);
				return RedirectToAction("PostDetails", "Post", new { id = model.PostId });
			}

			return View("CommentForm", model);
		}
	}
}