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
using AutoMapper;
using ForumSystem.Business.UserService;

namespace ForumSystemTests.PostServiceTest
{
	[TestClass]
	public class GetPostsCountShould : PostServiceTestShared
	{

		[TestMethod]
		public void GetUsersCount_Should_Return_Count_Of_Registred_Active_Users()
		{
			//Arrange
			int count = 10;

			//Act
			postRepoMock.Setup(postRepo => postRepo.GetPostsCount()).Returns(count);

			//Assert
			Assert.AreEqual(count, postServiceMock.GetPostsCount());
		}
	}
}
