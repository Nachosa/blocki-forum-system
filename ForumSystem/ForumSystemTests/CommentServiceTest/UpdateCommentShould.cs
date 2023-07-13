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
	public class UpdateCommentShould
	{
		[TestMethod]
		public void Update_Comment_Successfully_And_Return_Updated_Comment()
		{
			// Arrange
			int commentId = 1;
			var comment = new Comment { Id = commentId, Content = "Old content" };
			var updatedComment = new Comment { Id = commentId, Content = "New content" };

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

			commentRepositoryMock.Setup(cr => cr.UpdateCommentContent(comment, commentId)).Returns(updatedComment);

			// Act
			Comment result = sut.UpdateComment(comment, commentId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(updatedComment.Id, result.Id);
			Assert.AreEqual(updatedComment.Content, result.Content);
			commentRepositoryMock.Verify(cr => cr.UpdateCommentContent(comment, commentId), Times.Once);
		}
	}
}
