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

namespace ForumSystemTests.PostServiceTest
{
	[TestClass]
	public class UpdatePostContentShould : PostServiceTestShared
	{
		[TestMethod]
		public void Update_Post_Content_Successfully_And_Return_Updated_Comment()
		{
			// Arrange
			int postId = 1;
			string username = "TestUsername";
			var user = new User { Id = 1, Username = username };
			var post = new Post { Id = postId, UserId = 1, User = user, Content = "Old content" };
			var newPostContent = new Post { Content = "New content" };
			var updatedPost = new Post { Id = postId, UserId = 1, Content = "New content" };

			userRepoMock.Setup(userRepo => userRepo.GetUserByUserName(username)).Returns(user);
			postRepoMock.Setup(postRepo => postRepo.GetPostById(postId)).Returns(post);
			postRepoMock.Setup(postRepo => postRepo.UpdatePostContent(newPostContent, post)).Returns(updatedPost);

			// Act
			Post result = postServiceMock.UpdatePostContent(postId, newPostContent, username);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(updatedPost.Id, result.Id);
			Assert.AreEqual(updatedPost.Content, result.Content);
			postRepoMock.Verify(postRepo => postRepo.UpdatePostContent(newPostContent, post), Times.Once);
		}

		[TestMethod]
		public void Throw_UnauthorizedOperationException_When_Unauthorized_User()
		{
			// Arrange
			int postId = 1;
			string username = "TestUsername";
			string authorUsername = "TestAuthorUsername";
			var user = new User { Id = 3, Username = username };
			var author = new User { Id = 2, Username = authorUsername };
			var post = new Post { Id = postId, User = author, UserId = author.Id, Content = "Old content" };
			var newPostContent = new Post { Content = "New content" };

			userRepoMock.Setup(userRepo => userRepo.GetUserByUserName(username)).Returns(user);
			postRepoMock.Setup(postRepo => postRepo.GetPostById(postId)).Returns(post);

			// Act & Assert
			Assert.ThrowsException<UnauthenticatedOperationException>(() => postServiceMock.UpdatePostContent(postId, newPostContent, username));
			postRepoMock.Verify(postRepo => postRepo.UpdatePostContent(newPostContent, post), Times.Never);
		}
	}
}
