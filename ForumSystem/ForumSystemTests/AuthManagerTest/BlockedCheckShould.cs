using ForumSystem.Business.AuthenticationManager;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystemTests.AuthManagerTest
{
	[TestClass]
	public class BlockedCheckShould
	{
		[TestMethod]
		public void Return_True_When_User_Is_Blocked_By_User_Object()
		{
			// Arrange
			var user = new User { RoleId = 1 };

			var sut = new AuthManager(null);

			// Act
			bool result = sut.BlockedCheck(user);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Return_True_When_RoleId_Is_Blocked()
		{
			// Arrange
			int roleId = 1;

			var sut = new AuthManager(null);

			// Act
			bool result = sut.BlockedCheck(roleId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Return_False_When_RoleId_Is_Not_Blocked()
		{
			// Arrange
			int roleId = 2;

			var sut = new AuthManager(null);

			// Act
			bool result = sut.BlockedCheck(roleId);

			// Assert
			Assert.IsFalse(result);
		}
	}
}
