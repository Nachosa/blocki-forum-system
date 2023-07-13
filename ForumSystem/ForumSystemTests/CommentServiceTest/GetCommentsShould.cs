using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.Business;
using ForumSystem.DataAccess.CommentRepo;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
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
	public class GetCommentsShould
	{
		[TestMethod]
		public void Return_Comments_Successfully_When_Collection_Is_Not_Empty()
		{
			// Arrange
			var queryParameters = new CommentQueryParameters();
			var comments = new List<Comment>
			{
				new Comment { Id = 1, Content = "Comment 1" },
				new Comment { Id = 2, Content = "Comment 2" }
			};

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

			commentRepositoryMock.Setup(cr => cr.GetComments(queryParameters)).Returns(comments);

			// Act
			ICollection<Comment> result = sut.GetComments(queryParameters);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(comments.Count, result.Count);
			commentRepositoryMock.Verify(cr => cr.GetComments(queryParameters), Times.Once);
		}

		[TestMethod]
		public void Throw_EntityNotFoundException_When_Collection_Is_Empty()
		{
			// Arrange
			var queryParameters = new CommentQueryParameters();
			var comments = new List<Comment>();

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

			commentRepositoryMock.Setup(cr => cr.GetComments(queryParameters)).Returns(comments);

			// Act & Assert
			Assert.ThrowsException<EntityNotFoundException>(() => sut.GetComments(queryParameters));
			commentRepositoryMock.Verify(cr => cr.GetComments(queryParameters), Times.Once);
		}
	}
}
