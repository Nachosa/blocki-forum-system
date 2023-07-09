using AutoMapper;
using ForumSystem.DataAccess;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.UserRepo;
using ForumSystemDTO.UserDTO;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.PostRepo;

namespace ForumSystem.Business.UserService
{
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepo;
		private readonly IPostRepository postRepo;
		private readonly IMapper userMapper;
		public UserService(IUserRepository userRepo, IMapper createUserMapper, IPostRepository postRepo)
		{
			this.userRepo = userRepo;
			this.userMapper = createUserMapper;
			this.postRepo = postRepo;
		}
		public User CreateUser(User mappedUser)
		{
			if (userRepo.EmailExist(mappedUser.Email))
			{
				throw new EmailAlreadyExistException("Email already exists!");
			}
			if (userRepo.UsernameExist(mappedUser.Username))
			{
				throw new UsernameAlreadyExistException("Username already exists!");
			}

            mappedUser.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(mappedUser.Password));

			return userRepo.CreateUser(mappedUser);
		}
		public IEnumerable<User> GetAllUsers()
		{

			var allUsers = userRepo.GetAllUsers();

			if (!allUsers.Any())
			{
				throw new EntityNotFoundException("There aren't any users yet!");
			}

			return allUsers;
		}

		public int GetUsersCount()
		{
			var activeUsersCount = userRepo.GetUsersCount();
			return activeUsersCount;
		}

		public ICollection<Post> GetUserPosts(PostQueryParameters queryParams, int userId)
		{
			var User = userRepo.GetUserById(userId) ?? throw new EntityNotFoundException($"User with Id={userId} was not found!");
			var posts = postRepo.GetUserPosts(userId, queryParams);
			return posts;

		}

		public User GetUserById(int userId)
		{
			var originalUser = userRepo.GetUserById(userId) ?? throw new EntityNotFoundException($"User with Id={userId} was not found!");
			return originalUser;
		}

		public User GetUserByEmail(string email)
		{
			var originalUser = userRepo.GetUserByEmail(email) ?? throw new EntityNotFoundException($"User with Email={email} was not found!");
			return originalUser;
		}

		public User GetUserByUserName(string userName)
		{
			var originalUser = userRepo.GetUserByUserName(userName) ?? throw new EntityNotFoundException($"User with username:{userName} was not found!");
			return originalUser;
		}
        /// <summary>
        /// This method returns user with password hashed in BASE64.
        /// </summary>
       

		public List<User> SearchBy(UserQueryParams queryParams)
		{
			List<User> users;
			if (queryParams.Username is null &
				   queryParams.FirstName is null &
				   queryParams.Email is null)
			{
				users = userRepo.GetAllUsers().ToList();
			}
			else
			{
				users = userRepo.SearchBy(queryParams);
			}

			if (users.Count == 0)
			{
				throw new EntityNotFoundException($"User not found!");
			}
			return users;

		}

		public User UpdateUser(string userName, User userNewValues)
		{
			if (userNewValues.Email != null)
			{
				if (userRepo.EmailExist(userNewValues.Email))
				{
					throw new EmailAlreadyExistException("Email already exist!");
				}
			}
            var userToUpdate = userRepo.GetUserByUserName(userName);
			if (userToUpdate is null)
			{
				throw new EntityNotFoundException($"User with username:{userName} was not found!");
			}
			if (userNewValues.Password is not null)
			{
				userNewValues.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(userNewValues.Password));
			}
			var updatedUser = userRepo.UpdateUser(userName, userNewValues);
			return updatedUser;


		}

		//Тук може би също трябва да има проверка дали този, който едитва е притежателя на акаунта.
		public User UpdateUser(int id, User userNewValues)
		{
			if (userNewValues.Email != null)
			{
				if (userRepo.EmailExist(userNewValues.Email))
				{
					throw new EmailAlreadyExistException("Email already exist!");
				}
			}
			var userToUpdate = userRepo.GetUserById(id);
			if (userToUpdate is null)
			{
				throw new EntityNotFoundException($"User with username:{id} was not found!");
			}
			if (userNewValues.Password is not null)
			{
				userNewValues.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(userNewValues.Password));
			}
			var updatedUser = userRepo.UpdateUser(id, userNewValues);
			return updatedUser;


		}

		public bool DeleteUser(string userName, int? userId)
		{
			if (userId is not null)
			{
				var userToDelete = userRepo.GetUserById((int)userId);
				if (userToDelete is null) throw new EntityNotFoundException($"User with Id={userId} was not found!");
				return userRepo.DeleteUser(userToDelete);

			}
			else if (userName is not null)
			{
				var userToDelete = userRepo.GetUserByUserName(userName);
				if (userToDelete is null) throw new EntityNotFoundException($"User with username={userName} was not found!");
				return userRepo.DeleteUser(userToDelete);
			}
			throw new EntityNotFoundException("Please provide Id or Username for the user to be deleted!");
		}


	}
}
