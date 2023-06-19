﻿using AutoMapper;
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
    public class UpdateUserShould
    {
        [TestMethod]
        public void Update_When_Valid_Input()
        {
            User user = new User
            {
                FirstName = "TestFirstName"
               ,
                LastName = "TestLastName"
               ,
                Username = "TestUsername"
               ,
                Password = "1234567890"
                
            };
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo=>repo.GetUserByUserName(user.Username)).Returns(user);
            userRepoMock.Setup(repo=>repo.UpdateUser(user.Username,user)).Returns(user);

            var result = sut.UpdateUser(user.Username, user);
            Assert.AreEqual(result, user);

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
                Password = "1234567890"
                ,
                Email="test@mail.com"

            };
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo => repo.EmailExist(user.Email)).Returns(true);           


            Assert.ThrowsException<EmailAlreadyExistException>(() => sut.UpdateUser(user.Username, user));

        }
        [TestMethod]
        public void Throw_When_User_NotFound()
        {
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

            userRepoMock.Setup(repo => repo.EmailExist(user.Email)).Returns(false);
            userRepoMock.Setup(repo => repo.GetUserByUserName(user.Username));
            
            Assert.ThrowsException<EntityNotFoundException>(() => sut.UpdateUser(user.Username, user));

        }
    }
}
