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
    public class DeletePostShould
    {
        [TestMethod]
        public void Delete_When_Valid_PostID()
        {
            Post post = new Post
            {
                Id = 1,
                UserId=2,
                Title="BTC TRASH!!!!!!!!!!",

            };
            var adminRepoMock = new Mock<IAdminRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);

            postRepoMock.Setup(repo=>repo.GetPostById(It.IsAny<int>())).Returns(post);
            postRepoMock.Setup(repo=>repo.DeletePostById(It.IsAny<int>())).Returns(true);

            Assert.IsTrue(sut.DeletePost(1));

        }
        [TestMethod]
        public void Throw_When_Input_Is_Null()
        {
   
            var adminRepoMock = new Mock<IAdminRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() => sut.DeletePost(null));

        }

        [TestMethod]
        public void Throw_When_Post_NotFound()
        {
            
            var adminRepoMock = new Mock<IAdminRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            var postRepoMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var sut = new AdminService(userRepoMock.Object, postRepoMock.Object, mapperMock.Object, adminRepoMock.Object);

            postRepoMock.Setup(repo => repo.GetPostById(It.IsAny<int>()));

            Assert.ThrowsException<EntityNotFoundException>(() => sut.DeletePost(1));

        }
    }
}
