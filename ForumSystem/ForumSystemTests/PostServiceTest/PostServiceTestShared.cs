using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.TagRepo;
using ForumSystem.DataAccess.UserRepo;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemTests.PostServiceTest
{
	[TestClass]
	public class PostServiceTestShared
	{
		protected Mock<IPostRepository> postRepoMock;
		protected Mock<IUserRepository> userRepoMock;
		protected Mock<ITagRepository> tagRepoMock;
		protected Mock<IAuthManager> authManagerMock;
		protected PostService postServiceMock;

		[TestInitialize]
		public void TestInit()
		{
			postRepoMock = new Mock<IPostRepository>();
			userRepoMock = new Mock<IUserRepository>();
			tagRepoMock = new Mock<ITagRepository>();
			authManagerMock = new Mock<IAuthManager>();
			postServiceMock = new PostService(postRepoMock.Object, userRepoMock.Object, tagRepoMock.Object, authManagerMock.Object);
		}
	}
}
