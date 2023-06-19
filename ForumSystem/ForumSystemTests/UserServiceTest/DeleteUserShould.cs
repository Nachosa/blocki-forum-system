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
    public class DeleteUserShould
    {
        [TestMethod]
        public void Return_True_When_Valid_UserId()
        {
            int userId = 1;
            string username = null;

            User user = new User
            {
                FirstName = "TestFirstName"
               ,
                LastName = "TestLastName"
               ,
                Username = "TestUsername"
               ,
                Password = "1234567890"
                ,
                Email = "test@mail.com"

            };

            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserById(userId)).Returns(user);
            userRepoMock.Setup(repo => repo.DeleteUser(user)).Returns(true);

            Assert.IsTrue(sut.DeleteUser(username, userId));
        }

        [TestMethod]
        public void Throw_When_User_With_UserId_NotFound()
        {
            int userId = 1;
            string username = null;

            User user = new User
            {
                FirstName = "TestFirstName"
               ,
                LastName = "TestLastName"
               ,
                Username = "TestUsername"
               ,
                Password = "1234567890"
                ,
                Email = "test@mail.com"

            };

            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserById(userId));

            Assert.ThrowsException<EntityNotFoundException>(() => sut.DeleteUser(username, userId));
        }
        [TestMethod]
        public void Return_True_When_Valid_UserName()
        {
            int? userId = null;
            string username = "TestUsername";

            User user = new User
            {
                FirstName = "TestFirstName"
               ,
                LastName = "TestLastName"
               ,
                Username = "TestUsername"
               ,
                Password = "1234567890"
                ,
                Email = "test@mail.com"

            };

            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserByUserName(username)).Returns(user);
            userRepoMock.Setup(repo => repo.DeleteUser(user)).Returns(true);

            Assert.IsTrue(sut.DeleteUser(username, userId));
        }
        [TestMethod]
        public void Throw_When_User_With_UserName_NotFound()
        {
            int? userId = null;
            string username = "TestUsername";

            User user = new User
            {
                FirstName = "TestFirstName"
               ,
                LastName = "TestLastName"
               ,
                Username = "TestUsername"
               ,
                Password = "1234567890"
                ,
                Email = "test@mail.com"

            };

            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserByUserName(username));

            Assert.ThrowsException<EntityNotFoundException>(() => sut.DeleteUser(username, userId));

        }
        [TestMethod]
        public void Throw_When_Both_UserId_And_UserName_Are_Null()
        {
            int? userId = null;
            string username = null;

            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            Assert.ThrowsException<EntityNotFoundException>(() => sut.DeleteUser(username, userId));

        }
    }
}
