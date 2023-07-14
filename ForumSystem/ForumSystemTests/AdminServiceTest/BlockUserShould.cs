using AutoMapper;
using ForumSystem.Business.AdminService;
using ForumSystem.DataAccess.AdminRepo;
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

namespace ForumSystemTests.AdminServiceTest
{
    [TestClass]
    public class BlockUserShould
    {
        [TestMethod]
        public void Block_User_Admin_When_Valid_Id()
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
                Email = "test@mail.com",
                RoleId = 2

            };
            var adminRepoMock = new Mock<IAdminRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns(user);
            adminRepoMock.Setup(repo => repo.BlockUser(It.IsAny<User>())).Returns(true);

            Assert.IsTrue(sut.BlockUser(1, null));

        }
        [TestMethod]
        public void Throw_When_User_WithID_NotFound()
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
                Email = "test@mail.com",
                RoleId = 2

            };
            var adminRepoMock = new Mock<IAdminRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserById(It.IsAny<int>()));

            Assert.ThrowsException<EntityNotFoundException>(() => sut.BlockUser(1, null));

        }
        [TestMethod]
        public void Block_User_Admin_When_Valid_Email()
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
                Email = "test@mail.com",
                RoleId = 2

            };
            var adminRepoMock = new Mock<IAdminRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).Returns(user);
            adminRepoMock.Setup(repo => repo.BlockUser(It.IsAny<User>())).Returns(true);

            Assert.IsTrue(sut.BlockUser(null, "validMail"));

        }
        [TestMethod]
        public void Throw_When_User_WithEmail_NotFound()
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
                Email = "test@mail.com",
                RoleId = 2

            };
            var adminRepoMock = new Mock<IAdminRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);

            userRepoMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>()));

            Assert.ThrowsException<EntityNotFoundException>(() => sut.BlockUser(null, "validMail"));

        }
        [TestMethod]
        public void Throw_When_Both_Inputs_Are_Null_Or_Empty()
        {

            var adminRepoMock = new Mock<IAdminRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);


            Assert.ThrowsException<ArgumentNullException>(() => sut.BlockUser(null, null));

        }

        [TestMethod]
		public void Throw_When_User_WithID_Already_Blocked()
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
				Email = "test@mail.com",
				RoleId = 1

			};
			var adminRepoMock = new Mock<IAdminRepository>();
			var userRepoMock = new Mock<IUserRepository>();
			var postRepoMock = new Mock<IPostRepository>();
			var mapperMock = new Mock<IMapper>();
			var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);

			userRepoMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns(user);

            Assert.ThrowsException<EntityAlreadyBlockedException>(() => sut.BlockUser(1, null));
		}
		[TestMethod]
		public void Throw_When_User_WithEmail_Already_Blocked()
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
				Email = "test@mail.com",
				RoleId = 1

			};
			var adminRepoMock = new Mock<IAdminRepository>();
			var userRepoMock = new Mock<IUserRepository>();
			var postRepoMock = new Mock<IPostRepository>();
			var mapperMock = new Mock<IMapper>();
			var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);

			userRepoMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).Returns(user);

			Assert.ThrowsException<EntityAlreadyBlockedException>(() => sut.BlockUser(null, "validEmail"));
		}

	}
}
