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
	public class LikePostShould : PostServiceTestShared
	{
		[TestMethod]
		public void Create_Like_Successfully_When_Like_Does_Not_Exist()
		{
			// Arrange
			int postId = 1;
			string userName = "TestUser";
			var post = new Post { Id = postId };
			var user = new User { Id = 1, Username = userName };

			postRepoMock.Setup(postRepo => postRepo.GetPostById(postId)).Returns(post);
			userRepoMock.Setup(ur => ur.GetUserByUserName(userName)).Returns(user);
			postRepoMock.Setup(postRepo => postRepo.GetLike(postId, user.Id)).Returns((Like)null);

			// Act
			bool result = postServiceMock.LikePost(postId, userName);

			// Assert
			Assert.IsTrue(result);
			postRepoMock.Verify(postRepo => postRepo.CreateLike(post, user), Times.Once);
			postRepoMock.Verify(postRepo => postRepo.LikePost(It.IsAny<Like>()), Times.Never);
			postRepoMock.Verify(postRepo => postRepo.DeleteLike(It.IsAny<Like>()), Times.Never);
		}

		[TestMethod]
		public void Like_Post_Successfully_When_Like_Exists_And_Is_Deleted()
		{
			// Arrange
			int postId = 1;
			string userName = "TestUser";
			var post = new Post { Id = postId };
			var user = new User { Id = 1, Username = userName };
			var like = new Like { PostId = postId, UserId = user.Id, IsDeleted = true };

			postRepoMock.Setup(postRepo => postRepo.GetPostById(postId)).Returns(post);
			userRepoMock.Setup(ur => ur.GetUserByUserName(userName)).Returns(user);
			postRepoMock.Setup(postRepo => postRepo.GetLike(postId, user.Id)).Returns(like);

			// Act
			bool result = postServiceMock.LikePost(postId, userName);

			// Assert
			Assert.IsTrue(result);
			postRepoMock.Verify(postRepo => postRepo.CreateLike(It.IsAny<Post>(), It.IsAny<User>()), Times.Never);
			postRepoMock.Verify(postRepo => postRepo.LikePost(like), Times.Once);
			postRepoMock.Verify(postRepo => postRepo.DeleteLike(It.IsAny<Like>()), Times.Never);
		}

		[TestMethod]
		public void Delete_Like_Successfully_When_Like_Exists_And_Is_Not_Deleted()
		{
			// Arrange
			int postId = 1;
			string userName = "TestUser";
			var post = new Post { Id = postId };
			var user = new User { Id = 1, Username = userName };
			var like = new Like { PostId = postId, UserId = user.Id, IsDeleted = false };

			postRepoMock.Setup(postRepo => postRepo.GetPostById(postId)).Returns(post);
			userRepoMock.Setup(userRepo => userRepo.GetUserByUserName(userName)).Returns(user);
			postRepoMock.Setup(postRepo => postRepo.GetLike(postId, user.Id)).Returns(like);

			// Act
			bool result = postServiceMock.LikePost(postId, userName);

			// Assert
			Assert.IsTrue(result);
			postRepoMock.Verify(postRepo => postRepo.CreateLike(It.IsAny<Post>(), It.IsAny<User>()), Times.Never);
			postRepoMock.Verify(postRepo => postRepo.LikePost(It.IsAny<Like>()), Times.Never);
			postRepoMock.Verify(postRepo => postRepo.DeleteLike(like), Times.Once);
		}
	}
}
