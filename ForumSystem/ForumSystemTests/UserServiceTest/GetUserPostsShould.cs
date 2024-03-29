﻿using AutoMapper;
using ForumSystem.Api.QueryParams;
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
    public class GetUserPostsShould
    {
        [TestMethod]
        public void Return_UserPosts()
        {
            var queryParams = new PostQueryParameters();
            List<Post> posts = new List<Post>();
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
                .Setup(repo=>repo.GetUserById(user.Id))
                .Returns(user);

            postRepoMock
                .Setup(repo => repo.GetUserPosts(user.Id, queryParams))
                .Returns(posts);

            var result=sut.GetUserPosts(queryParams, user.Id);
            Assert.AreEqual(result, posts);


        }
        [TestMethod]
        public void Throw_When_User_NotFound()
        {
            var queryParams = new PostQueryParameters();
            List<Post> posts = new List<Post>();
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
                .Setup(repo => repo.GetUserById(user.Id));


            postRepoMock
                .Setup(repo => repo.GetUserPosts(user.Id, queryParams))
                .Returns(posts);

            Assert.ThrowsException<EntityNotFoundException>(() => sut.GetUserPosts(queryParams, user.Id));


        }
    }
}
