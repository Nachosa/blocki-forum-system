using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemTests.AuthManagerTest
{
    [TestClass]
    public class AuthManagerShould
    {
        [TestMethod]
        public void Retun_User_When_Valid_Input()
        {
            User user = new User
            {
                FirstName = "TestFirstName"
                ,
                LastName = "TestLastName"
                ,
                Username = "TestUsername"
                ,
                Password = "MTIz",
                Email = "test@mail.com"

            };

            var userServiceMock = new Mock<IUserService>();
            var sut =new AuthManager(userServiceMock.Object);

            userServiceMock.Setup(repo=>repo.GetUserByUserName(It.IsAny<string>())).Returns(user);

            var result = sut.UserCheck("test:123");
            Assert.AreEqual(user, result);  

        }

        [TestMethod]
        public void Throw_When_Credentials_Are_Null()
        {
            var userServiceMock = new Mock<IUserService>();
            var sut = new AuthManager(userServiceMock.Object);


            Assert.ThrowsException<UnauthenticatedOperationException>(() => sut.UserCheck(null));
        }
        [TestMethod]
        public void Throw_When_Password_Is_Wrong()
        {
            User user = new User
            {
                FirstName = "TestFirstName"
               ,
                LastName = "TestLastName"
               ,
                Username = "TestUsername"
               ,
                Password = "MTIz",
                Email = "test@mail.com"

            };

            var userServiceMock = new Mock<IUserService>();
            var sut = new AuthManager(userServiceMock.Object);

            userServiceMock.Setup(repo => repo.GetUserByUserName(It.IsAny<string>())).Returns(user);

            Assert.ThrowsException<UnauthenticatedOperationException>(() => sut.UserCheck("test:wrongPassword"));

        }

        [TestMethod]
        public void AdminCheck_Should_Throw_When_User_Not_Admin()
        {

            User user = new User
            {
                FirstName = "TestFirstName"
                ,
                LastName = "TestLastName"
                ,
                Username = "TestUsername"
                ,
                Password = "MTIz",
                Email = "test@mail.com",
                RoleId=2

            };

            var userServiceMock = new Mock<IUserService>();
            var sut = new AuthManager(userServiceMock.Object);

            userServiceMock.Setup(repo => repo.GetUserByUserName(It.IsAny<string>())).Returns(user);

            Assert.ThrowsException<UnauthorizedAccessException>(()=>sut.AdminCheck("test:123"));
            
        }
        [TestMethod]
        public void BlockedCheck_Should_Throw_When_User_Not_Admin()
        {

            User user = new User
            {
                FirstName = "TestFirstName"
                ,
                LastName = "TestLastName"
                ,
                Username = "TestUsername"
                ,
                Password = "MTIz",
                Email = "test@mail.com",
                RoleId = 1

            };

            var userServiceMock = new Mock<IUserService>();
            var sut = new AuthManager(userServiceMock.Object);

            userServiceMock.Setup(repo => repo.GetUserByUserName(It.IsAny<string>())).Returns(user);

            Assert.ThrowsException<UnauthorizedAccessException>(() => sut.BlockedCheck("test:123"));

        }
    }
}
