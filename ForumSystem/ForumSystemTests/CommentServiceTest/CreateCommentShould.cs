using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.Business;
using ForumSystem.DataAccess.CommentRepo;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.UserRepo;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemTests.CommentServiceTest
{
	[TestClass]
	public class CreateCommentShould
	{
		[TestMethod]
		public void Create_Comment_Successfully_And_Return_The_Created_Comment()
		{
			// Arrange
			int postId = 1;
			var comment = new Comment { Id = 1, Content = "Test comment" };
			var post = new Post { Id = postId, Title = "Test post", Comments = new List<Comment>() };

			var authManagerMock = new Mock<IAuthManager>();
			var commentRepositoryMock = new Mock<ICommentRepository>();
			var postServiceMock = new Mock<IPostService>();
			var userServiceMock = new Mock<IUserService>();
			var userRepositoryMock = new Mock<IUserRepository>();

			var sut = new CommentService(
				authManagerMock.Object,
				commentRepositoryMock.Object,
				postServiceMock.Object,
				userServiceMock.Object,
				userRepositoryMock.Object
			);

			postServiceMock.Setup(ps => ps.GetPostById(postId)).Returns(post);
			commentRepositoryMock.Setup(cr => cr.CreateComment(comment)).Returns(comment);

			// Act
			Comment createdComment = sut.CreateComment(comment, postId);

			// Assert
			Assert.IsNotNull(createdComment);
			Assert.AreEqual(comment.Id, createdComment.Id);
			Assert.AreEqual(comment.Content, createdComment.Content);
			Assert.AreEqual(post.Comments.Count, 1);
			Assert.IsTrue(post.Comments.Contains(createdComment));
			commentRepositoryMock.Verify(cr => cr.CreateComment(comment), Times.Once);
		}

		[TestMethod]
		public void Throw_EntityNotFoundException_When_Post_Not_Found()
		{
			// Arrange
			int postId = 1;
			var comment = new Comment { Id = 1, Content = "Test comment" };

			var authManagerMock = new Mock<IAuthManager>();
			var commentRepositoryMock = new Mock<ICommentRepository>();
			var postServiceMock = new Mock<IPostService>();
			var userServiceMock = new Mock<IUserService>();
			var userRepositoryMock = new Mock<IUserRepository>();

			var sut = new CommentService(
				authManagerMock.Object,
				commentRepositoryMock.Object,
				postServiceMock.Object,
				userServiceMock.Object,
				userRepositoryMock.Object
			);

			postServiceMock.Setup(ps => ps.GetPostById(postId)).Throws(new EntityNotFoundException($"Post with id={postId} doesn't exist."));

			// Act & Assert
			Assert.ThrowsException<EntityNotFoundException>(() => sut.CreateComment(comment, postId));
			commentRepositoryMock.Verify(cr => cr.CreateComment(comment), Times.Never);
		}
	}
}
