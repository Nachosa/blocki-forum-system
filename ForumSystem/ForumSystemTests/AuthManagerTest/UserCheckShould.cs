using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.UserRepo;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemTests.AuthManagerTest
{
	[TestClass]
	public class UserCheckShould
	{
		[TestMethod]
		public void Return_User_Successfully_When_Valid_Username_And_Password()
		{
			// Arrange
			string userName = "TestUser";
			string password = "TestPassword";
			var encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
			var user = new User { Username = userName, Password = encodedPassword };

			var userServiceMock = new Mock<IUserService>();

			var sut = new AuthManager(userServiceMock.Object);

			userServiceMock.Setup(us => us.GetUserByUserName(userName)).Returns(user);

			// Act
			User result = sut.UserCheck(userName, password);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(user, result);
		}

		[TestMethod]
		public void Throw_UnauthenticatedOperationException_When_Invalid_Username_Or_Password()
		{
			// Arrange
			string userName = "TestUser";
			string password = "TestPassword";
			var user = new User { Username = userName, Password = "IncorrectPassword" };

			var userServiceMock = new Mock<IUserService>();

			var sut = new AuthManager(userServiceMock.Object);

			userServiceMock.Setup(us => us.GetUserByUserName(userName)).Returns(user);

			// Act & Assert
			Assert.ThrowsException<UnauthenticatedOperationException>(() => sut.UserCheck(userName, password));
		}
	}
}
