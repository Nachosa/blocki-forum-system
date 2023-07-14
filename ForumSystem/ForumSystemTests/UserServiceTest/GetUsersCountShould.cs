using AutoMapper;
using ForumSystem.Business.UserService;
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
	public class GetUsersCountShould
	{
		[TestMethod]
		public void GetUsersCount_Should_Return_Count_Of_Registred_Active_Users()
		{
			int count = 10;
			var userRepoMock = new Mock<IUserRepository>();
			var postRepoMock = new Mock<IPostRepository>();
			var mapperMock = new Mock<IMapper>();
			var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

			userRepoMock.Setup(repo => repo.GetUsersCount()).Returns(count);

			Assert.AreEqual(count,sut.GetUsersCount());

		}
	}
}
