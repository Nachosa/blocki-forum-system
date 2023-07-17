using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.TagRepo;
using ForumSystem.DataAccess.UserRepo;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Models;
using System.Xml.Linq;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.CommentRepo;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.QueryParams;

namespace ForumSystemTests.PostServiceTest
{
	[TestClass]
	public class GetPostShould : PostServiceTestShared
	{

		[TestMethod]
		public void Return_List_Of_Posts_When_Valid_QueryParams()
		{
			// Arrange
			var queryParams = new PostQueryParameters { Title = "Test" };
			var posts = new List<Post>
		{
			new Post { Id = 1, Title = "TestTitle1", Content = "TestContent1" },
			new Post { Id = 2, Title = "TestTitle2", Content = "TestContent2" }
		};
			postRepoMock.Setup(repo => repo.GetPosts(queryParams)).Returns(posts);

			// Act
			var result = postServiceMock.GetPosts(queryParams);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(posts.Count, result.Count);
			postRepoMock.Verify(postRepo => postRepo.GetPosts(queryParams), Times.Once);
		}
	}
}
