using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.Business;
using ForumSystem.DataAccess.CommentRepo;
using ForumSystem.DataAccess.UserRepo;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;

namespace ForumSystemTests.CommentServiceTest
{
	[TestClass]
	public class DeleteCommentByIdShould
	{
		[TestMethod]
		public void Return_True_When_Valid_CommentId_And_Author()
		{
			int commentId = 1;
			string username = "TestUsername";
			var comment = new Comment { Id = commentId, UserId = 1 };
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
			commentRepositoryMock.Setup(cr => cr.GetCommentById(commentId)).Returns(comment);
			commentRepositoryMock.Setup(cr => cr.DeleteCommentById(commentId)).Returns(true);

			bool result = sut.DeleteCommentById(commentId, username);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Throw_UnauthorizedOperationException_When_Unauthorized_User()
		{
			int commentId = 1;
			string username = "TestUsername";
			var comment = new Comment { Id = commentId, UserId = 1 };
			var user = new User { Id = 2, Username = username };

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
			commentRepositoryMock.Setup(cr => cr.GetCommentById(commentId)).Returns(comment);

			Assert.ThrowsException<UnauthorizedOperationException>(() => sut.DeleteCommentById(commentId, username));
		}

		[TestMethod]
		public void Throw_EntityNotFoundException_When_Comment_Not_Found()
		{
			int commentId = 1;
			string username = "TestUsername";

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

			commentRepositoryMock.Setup(cr => cr.GetCommentById(commentId)).Returns((Comment)null);

			Assert.ThrowsException<EntityNotFoundException>(() => sut.DeleteCommentById(commentId, username));
		}
	}
}