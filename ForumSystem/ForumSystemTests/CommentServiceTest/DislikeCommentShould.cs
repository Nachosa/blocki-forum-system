using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.Business;
using ForumSystem.DataAccess.CommentRepo;
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
	public class DislikeCommentShould
	{
		[TestMethod]
		public void Create_Dislike_Successfully_When_Dislike_Does_Not_Exist()
		{
			// Arrange
			int commentId = 1;
			string userName = "TestUser";
			var comment = new Comment { Id = commentId };
			var user = new User { Id = 1, Username = userName };

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

			commentRepositoryMock.Setup(cr => cr.GetCommentById(commentId)).Returns(comment);
			userRepositoryMock.Setup(ur => ur.GetUserByUserName(userName)).Returns(user);
			commentRepositoryMock.Setup(cr => cr.GetLike(commentId, user.Id)).Returns((Like)null);

			// Act
			bool result = sut.DislikeComment(commentId, userName);

			// Assert
			Assert.IsTrue(result);
			commentRepositoryMock.Verify(cr => cr.CreateLike(comment, user), Times.Once);
			commentRepositoryMock.Verify(cr => cr.DislikeComment(It.IsAny<Like>()), Times.Never);
			commentRepositoryMock.Verify(cr => cr.DeleteLike(It.IsAny<Like>()), Times.Never);
		}

		[TestMethod]
		public void Dislike_Comment_Successfully_When_Dislike_Exists_And_Is_Deleted()
		{
			// Arrange
			int commentId = 1;
			string userName = "TestUser";
			var comment = new Comment { Id = commentId };
			var user = new User { Id = 1, Username = userName };
			var dislike = new Like { CommentId = commentId, UserId = user.Id, IsDeleted = true, IsDislike = true };

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

			commentRepositoryMock.Setup(cr => cr.GetCommentById(commentId)).Returns(comment);
			userRepositoryMock.Setup(ur => ur.GetUserByUserName(userName)).Returns(user);
			commentRepositoryMock.Setup(cr => cr.GetLike(commentId, user.Id)).Returns(dislike);

			// Act
			bool result = sut.DislikeComment(commentId, userName);

			// Assert
			Assert.IsTrue(result);
			commentRepositoryMock.Verify(cr => cr.CreateLike(It.IsAny<Comment>(), It.IsAny<User>()), Times.Never);
			commentRepositoryMock.Verify(cr => cr.DislikeComment(dislike), Times.Once);
			commentRepositoryMock.Verify(cr => cr.DeleteLike(It.IsAny<Like>()), Times.Never);
		}

		[TestMethod]
		public void Delete_Dislike_Successfully_When_Dislike_Exists_And_Is_Not_Deleted()
		{
			// Arrange
			int commentId = 1;
			string userName = "TestUser";
			var comment = new Comment { Id = commentId };
			var user = new User { Id = 1, Username = userName };
			var dislike = new Like { CommentId = commentId, UserId = user.Id, IsDeleted = false, IsDislike = true };

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

			commentRepositoryMock.Setup(cr => cr.GetCommentById(commentId)).Returns(comment);
			userRepositoryMock.Setup(ur => ur.GetUserByUserName(userName)).Returns(user);
			commentRepositoryMock.Setup(cr => cr.GetLike(commentId, user.Id)).Returns(dislike);

			// Act
			bool result = sut.DislikeComment(commentId, userName);

			// Assert
			Assert.IsTrue(result);
			commentRepositoryMock.Verify(cr => cr.CreateLike(It.IsAny<Comment>(), It.IsAny<User>()), Times.Never);
			commentRepositoryMock.Verify(cr => cr.DislikeComment(It.IsAny<Like>()), Times.Never);
			commentRepositoryMock.Verify(cr => cr.DeleteLike(dislike), Times.Once);
		}
	}
}
