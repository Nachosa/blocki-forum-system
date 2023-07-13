using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemTests.AuthManagerTest
{
	[TestClass]
	public class AdminCheckShould
	{
		[TestMethod]
		public void Return_True_When_User_Is_Admin_By_User_Object()
		{
			// Arrange
			var user = new User { RoleId = 3 };

			var sut = new AuthManager(null);

			// Act
			bool result = sut.AdminCheck(user);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Return_True_When_RoleId_Is_Admin()
		{
			// Arrange
			int roleId = 3;

			var sut = new AuthManager(null);

			// Act
			bool result = sut.AdminCheck(roleId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Return_False_When_RoleId_Is_Not_Admin()
		{
			// Arrange
			int roleId = 2;

			var sut = new AuthManager(null);

			// Act
			bool result = sut.AdminCheck(roleId);

			// Assert
			Assert.IsFalse(result);
		}
	}
}
