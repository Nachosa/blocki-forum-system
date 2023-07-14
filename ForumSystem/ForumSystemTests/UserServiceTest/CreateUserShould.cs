using AutoMapper;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.UserRepo;
using ForumSystemDTO.UserDTO;
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
    public class CreateUserShould
    {
        [TestMethod]
        public void Create_When_Valid_Input()
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

            userRepoMock
                .Setup(repo => repo.EmailExist(It.IsAny<string>()))
                .Returns(false);

            userRepoMock
                .Setup(repo => repo.CreateUser(user))
                .Returns(user);

            

            var result=sut.CreateUser(user);
            
            Assert.AreEqual(user, result);


        }

        [TestMethod]
        public void Throw_When_Email_Exist()
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

            userRepoMock
                .Setup(repo => repo.EmailExist(It.IsAny<string>()))
                .Returns(true);

            userRepoMock
                .Setup(repo => repo.CreateUser(user))
                .Returns(user);

     
            Assert.ThrowsException<EmailAlreadyExistException>(() => sut.CreateUser(user));
        }

		[TestMethod]
		public void Throw_When_Username_Exist()
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

			userRepoMock
				.Setup(repo => repo.EmailExist(It.IsAny<string>()))
				.Returns(false);

			userRepoMock
				.Setup(repo => repo.UsernameExist(It.IsAny<string>()))
				.Returns(true);


			Assert.ThrowsException<UsernameAlreadyExistException>(() => sut.CreateUser(user));
		}

	}
}
