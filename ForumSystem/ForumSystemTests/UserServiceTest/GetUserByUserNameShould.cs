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
    public class GetUserByUserNameShould
    {
        [TestMethod]
        public void Return_User()
        {
            User user = new User
            {
                FirstName = "TestFirstName"
                ,
                LastName = "TestLastName"
                ,
                Username = "TestUsername"
                ,
                Password = "1234567890",
                Email = "test@mail.com"

            };

            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserByUserName(user.Username)).Returns(user);

            var result = sut.GetUserByUserName(user.Username);

            Assert.AreEqual(user, result);

        }
        [TestMethod]
        public void Throw_When_NotFound()
        {
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserByUserName(It.IsAny<string>()));

            Assert.ThrowsException<EntityNotFoundException>(() => sut.GetUserByUserName(It.IsAny<string>()));

        }
    }
}

