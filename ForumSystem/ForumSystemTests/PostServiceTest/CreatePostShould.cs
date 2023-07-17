using AutoMapper;
using ForumSystem.Business;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.UserRepo;
using ForumSystem.DataAccess.TagRepo;
using ForumSystemDTO.UserDTO;
using ForumSystemDTO.PostDTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.Business.AuthenticationManager;

namespace ForumSystemTests.PostServiceTest
{
    [TestClass]
    public class CreatePostShould : PostServiceTestShared
    {

		[TestMethod]
        public void Create_When_Valid_Input()
        {

            User user = new User
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Username = "TestUsername",
                Password = "1234567890",
                Email = "test@mail.com"
            };

            Post post = new Post
            {
                UserId = user.Id,
                Title = "TestTitle",
                Content = "Don't mind this content, just testing!",
            };

            userRepoMock
                .Setup(repo => repo.GetUserByUserName(user.Username))
                .Returns(user);        

            var result = postServiceMock.CreatePost(post, user.Username);

            Assert.AreEqual(post, result);
        }
	}
}
