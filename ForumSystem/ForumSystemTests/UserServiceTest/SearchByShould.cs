using AutoMapper;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.QueryParams;
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
    public class SearchByShould
    {
        [TestMethod]
        public void Return_Users()
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

            var queryParams = new UserQueryParams();

            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo => repo.SearchBy(queryParams)).Returns(users);

            var result = sut.SearchBy(queryParams);

            Assert.AreEqual(users, result);

        }
        [TestMethod]
        public void Throw_When_UsersCount_is_zero()
        {
            List<User> users = new List<User>();
          

            var queryParams = new UserQueryParams();

            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new UserService(userRepoMock.Object, mapperMock.Object, postRepoMock.Object);

            userRepoMock.Setup(repo => repo.SearchBy(queryParams)).Returns(users);

            Assert.ThrowsException<EntityNotFoundException>(() => sut.SearchBy(queryParams));

        }
    }
}
