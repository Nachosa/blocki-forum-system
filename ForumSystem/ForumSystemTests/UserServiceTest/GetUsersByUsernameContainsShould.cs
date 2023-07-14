using AutoMapper;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.UserRepo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemTests.UserServiceTest
{
	[TestClass]
	public class GetUsersByUsernameContainsShould
	{
		[TestMethod]
		public void GetUsersByUsernameContains_Should_Retrun_Users()
		{
			List<User> users = new List<User>()
			{
				 new User
				 {
					 FirstName = "TestFirstName"
					 ,
					 LastName = "TestLastName"
						 ,
					  Username = "TestUsername"
					   ,
					   Password = "1234567890",
					 Email = "test@mail.com"

				 },
				 new User
				 {
					 FirstName = "TestFirstName"
					 ,
					 LastName = "TestLastName"
						 ,
					  Username = "TestUsername"
					   ,
					   Password = "1234567890",
					 Email = "test2@mail.com"

				 }

			};
			var userRepoMock = new Mock<IUserRepository>();
			var postRepoMock = new Mock<IPostRepository>();
			var mapperMock = new Mock<IMapper>();
			var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

			userRepoMock.Setup(repo => repo.GetUsersByUsernameContains(It.IsAny<string>())).Returns(users);

			Assert.AreEqual(users, sut.GetUsersByUsernameContains("valid"));

		}
		[TestMethod]
		public void GetUsersByUsernameContains_Should_Throw_When_Empty_Result()
		{
			List<User> users = new List<User>()
			{

			};
			var userRepoMock = new Mock<IUserRepository>();
			var postRepoMock = new Mock<IPostRepository>();
			var mapperMock = new Mock<IMapper>();
			var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

			userRepoMock.Setup(repo => repo.GetUsersByUsernameContains(It.IsAny<string>())).Returns(users);

			Assert.ThrowsException<EntityNotFoundException>(()=> sut.GetUsersByUsernameContains("valid"));

		}
	}
}
