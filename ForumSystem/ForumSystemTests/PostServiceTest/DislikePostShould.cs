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

namespace ForumSystemTests.PostServiceTest
{
	[TestClass]
	public class DislikePostShould : PostServiceTestShared
	{
		[TestMethod]
		public void Create_Dislike_Successfully_When_Dislike_Does_Not_Exist()
		{
			// Arrange
			int postId = 1;
			string userName = "TestUser";
			var post = new Post { Id = postId };
			var user = new User { Id = 1, Username = userName };

			postRepoMock.Setup(postRepo => postRepo.GetPostById(postId)).Returns(post);
			userRepoMock.Setup(userRepo => userRepo.GetUserByUserName(userName)).Returns(user);
			postRepoMock.Setup(postRepo => postRepo.GetLike(postId, user.Id)).Returns((Like)null);

			// Act
			bool result = postServiceMock.DislikePost(postId, userName);

			// Assert
			Assert.IsTrue(result);
			postRepoMock.Verify(postRepo => postRepo.CreateLike(post, user), Times.Once);
			postRepoMock.Verify(postRepo => postRepo.DislikePost(It.IsAny<Like>()), Times.Never);
			postRepoMock.Verify(postRepo => postRepo.DeleteLike(It.IsAny<Like>()), Times.Never);
		}

		[TestMethod]
		public void Dislike_Post_Successfully_When_Dislike_Exists_And_Is_Deleted()
		{
			// Arrange
			int postId = 1;
			string userName = "TestUser";
			var post = new Post { Id = postId };
			var user = new User { Id = 1, Username = userName };
			var dislike = new Like { CommentId = postId, UserId = user.Id, IsDeleted = true, IsDislike = true };

			postRepoMock.Setup(postRepo => postRepo.GetPostById(postId)).Returns(post);
			userRepoMock.Setup(userRepo => userRepo.GetUserByUserName(userName)).Returns(user);
			postRepoMock.Setup(postRepo => postRepo.GetLike(postId, user.Id)).Returns(dislike);

			// Act
			bool result = postServiceMock.DislikePost(postId, userName);

			// Assert
			Assert.IsTrue(result);
			postRepoMock.Verify(postRepo => postRepo.CreateLike(It.IsAny<Post>(), It.IsAny<User>()), Times.Never);
			postRepoMock.Verify(postRepo => postRepo.DislikePost(dislike), Times.Once);
			postRepoMock.Verify(postRepo => postRepo.DeleteLike(It.IsAny<Like>()), Times.Never);
		}

		[TestMethod]
		public void Delete_Dislike_Successfully_When_Dislike_Exists_And_Is_Not_Deleted()
		{
			// Arrange
			int postId = 1;
			string userName = "TestUser";
			var post = new Post { Id = postId };
			var user = new User { Id = 1, Username = userName };
			var dislike = new Like { CommentId = postId, UserId = user.Id, IsDeleted = false, IsDislike = true };

			postRepoMock.Setup(postRepo => postRepo.GetPostById(postId)).Returns(post);
			userRepoMock.Setup(userRepo => userRepo.GetUserByUserName(userName)).Returns(user);
			postRepoMock.Setup(postRepo => postRepo.GetLike(postId, user.Id)).Returns(dislike);

			// Act
			bool result = postServiceMock.DislikePost(postId, userName);

			// Assert
			Assert.IsTrue(result);
			postRepoMock.Verify(postRepo => postRepo.CreateLike(It.IsAny<Post>(), It.IsAny<User>()), Times.Never);
			postRepoMock.Verify(postRepo => postRepo.DislikePost(It.IsAny<Like>()), Times.Never);
			postRepoMock.Verify(postRepo => postRepo.DeleteLike(dislike), Times.Once);
		}
	}
}
