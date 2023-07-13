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
	public class UpdateCommentContentShould
	{
		[TestMethod]
		public void Update_Comment_Content_Successfully_And_Return_Updated_Comment()
		{
			// Arrange
			int commentId = 1;
			string username = "TestUsername";
			var comment = new Comment { Id = commentId, UserId = 1, Content = "Old content" };
			var updatedComment = new Comment { Id = commentId, UserId = 1, Content = "New content" };
			var user = new User { Id = 1, Username = username };

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

			userServiceMock.Setup(us => us.GetUserByUserName(username)).Returns(user);
			commentRepositoryMock.Setup(cr => cr.UpdateCommentContent(comment, commentId)).Returns(updatedComment);

			// Act
			Comment result = sut.UpdateCommentContent(comment, commentId, username);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(updatedComment.Id, result.Id);
			Assert.AreEqual(updatedComment.Content, result.Content);
			commentRepositoryMock.Verify(cr => cr.UpdateCommentContent(comment, commentId), Times.Once);
		}

		[TestMethod]
		public void Throw_UnauthorizedOperationException_When_Unauthorized_User()
		{
			// Arrange
			int commentId = 1;
			string username = "TestUsername";
			var comment = new Comment { Id = commentId, UserId = 2, Content = "Old content" };
			var user = new User { Id = 3, Username = username };

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

			userServiceMock.Setup(us => us.GetUserByUserName(username)).Returns(user);

			// Act & Assert
			Assert.ThrowsException<UnauthorizedOperationException>(() => sut.UpdateCommentContent(comment, commentId, username));
			commentRepositoryMock.Verify(cr => cr.UpdateCommentContent(comment, commentId), Times.Never);
		}
	}
}
