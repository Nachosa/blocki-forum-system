using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using Microsoft.EntityFrameworkCore;

namespace ForumSystem.DataAccess.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumSystemContext forumDb;

        public UserRepository(ForumSystemContext forumDb)
        {
            this.forumDb = forumDb;
        }

        public User CreateUser(User user)
        {
            user.RoleId = 2;
            forumDb.Users.Add(user);
            forumDb.SaveChanges();
            return user;
        }
        //Retrun all NOT DELETED USERS!
        public IEnumerable<User> GetAllUsers()
        {
            var result = forumDb.Users
                .Include(p => p.Posts.Where(p => p.IsDeleted == false))
                    .ThenInclude(p => p.Tags.Where(pt => pt.Tag.IsDeleted == false))
				.Include(p => p.Posts.Where(p => p.IsDeleted == false))
                    .ThenInclude(p => p.Likes.Where(l => l.IsDeleted == false))
				.Include(c => c.Comments).Where(c => c.IsDeleted == false).Where(u => u.IsDeleted == false).ToList();

            return result;
        }

        public User GetUserById(int Id)
        {
            var user = forumDb.Users.Include(p => p.Posts.Where(p => p.IsDeleted == false))
                                        .ThenInclude(p => p.Tags.Where(pt => pt.Tag.IsDeleted == false))
				                    .Include(p => p.Posts.Where(p => p.IsDeleted == false))
                                        .ThenInclude(p => p.Likes.Where(l => l.IsDeleted == false))
									.Include(u => u.Comments.Where(c => c.IsDeleted == false))
                                        .ThenInclude(c => c.Likes.Where(l => l.IsDeleted == false))

									.FirstOrDefault(u => u.Id == Id && u.IsDeleted == false);
            return user;
        }

        public User GetUserByUserName(string Username)
        {
            var userWithThatUserName = forumDb.Users.Include(p => p.Posts.Where(p => p.IsDeleted == false))
										.ThenInclude(p => p.Tags.Where(pt => pt.Tag.IsDeleted == false))
									.Include(p => p.Posts.Where(p => p.IsDeleted == false))
										.ThenInclude(p => p.Likes.Where(l => l.IsDeleted == false))
									.Include(u => u.Comments.Where(c => c.IsDeleted == false))
										.ThenInclude(c => c.Likes.Where(l => l.IsDeleted == false))

									.FirstOrDefault(u => u.Username == Username && u.IsDeleted == false);
            return userWithThatUserName;
        }
		public List<User> GetUsersByUsernameContains(string input)
		{
			var usersWhichUsernameCointainsInput = forumDb.Users
                                            .Include(p => p.Posts.Where(p => p.IsDeleted == false))
                                                .ThenInclude(p => p.Tags.Where(pt => pt.Tag.IsDeleted == false))
									        .Include(p => p.Posts.Where(p => p.IsDeleted == false))
                                                .ThenInclude(p => p.Likes.Where(l => l.IsDeleted == false))
									        .Include(c => c.Comments).Where(c => c.IsDeleted == false)
									            .Where(u => u.Username.ToLower().Contains(input.ToLower()) && u.IsDeleted == false);
            return usersWhichUsernameCointainsInput.ToList();
		}

        public User GetUserByEmail(string email)
        {
            var userWithThatEmail = forumDb.Users.Include(p => p.Posts.Where(p => p.IsDeleted == false))
										.ThenInclude(p => p.Tags.Where(pt => pt.Tag.IsDeleted == false))
									.Include(p => p.Posts.Where(p => p.IsDeleted == false))
										.ThenInclude(p => p.Likes.Where(l => l.IsDeleted == false))
									.Include(u => u.Comments.Where(c => c.IsDeleted == false))
										.ThenInclude(c => c.Likes.Where(l => l.IsDeleted == false))

									.FirstOrDefault(u => u.Email == email && u.IsDeleted == false);
            return userWithThatEmail;
        }

        public IEnumerable<User> GetUsersByFirstName(string firstName)
        {
            var usersWithThatName = forumDb.Users
                                    .Include(p => p.Posts.Where(p => p.IsDeleted == false))
                                        .ThenInclude(p => p.Tags.Where(pt => pt.Tag.IsDeleted == false))
									.Include(p => p.Posts.Where(p => p.IsDeleted == false))
                                        .ThenInclude(p => p.Likes.Where(l => l.IsDeleted == false))
									.Include(c => c.Comments).Where(c => c.IsDeleted == false)
                                        .Where(u => u.FirstName == firstName && u.IsDeleted == false);
            return usersWithThatName;
        }

        public int GetUsersCount()
        {
            var activeUsersCount = forumDb.Users.Count(u => u.IsDeleted == false);
            return activeUsersCount;

        }

        public List<User> SearchBy(UserQueryParams queryParams)
        {
            var query = forumDb.Users
                .Include(p => p.Posts.Where(p => p.IsDeleted == false))
                    .ThenInclude(p => p.Tags.Where(pt => pt.Tag.IsDeleted == false))
                .Include(p => p.Posts.Where(p => p.IsDeleted == false))
                    .ThenInclude(p => p.Likes.Where(l => l.IsDeleted == false))
                .Include(c => c.Comments)
                .Where(c => c.IsDeleted == false);
                //.Where(u => u.IsDeleted == false);

            if (queryParams.FirstName is not null)
            {
                query = query.Where(u => u.FirstName.ToLower() == queryParams.FirstName.ToLower());
            }

            if (queryParams.Username is not null)
            {
                query = query.Where(u => u.Username == queryParams.Username);
            }

            if (queryParams.Email is not null)
            {
                query = query.Where(u => u.Email == queryParams.Email);
            }

            var result = query.ToList();
            return result;

        }


        public User UpdateUser(string userName, User user)
        {
            var userToupdate = forumDb.Users.FirstOrDefault(u => u.Username == userName);
            userToupdate.FirstName = user.FirstName ?? userToupdate.FirstName;
            userToupdate.LastName = user.LastName ?? userToupdate.LastName;
            userToupdate.Email = user.Email ?? userToupdate.Email;
            userToupdate.Password = user.Password ?? userToupdate.Password;
            forumDb.SaveChanges();
            return userToupdate;
        }
        public User UpdateUser(int id, User user)
        {
            var userToUpdate = forumDb.Users.FirstOrDefault(u => u.Id == id);
            userToUpdate.FirstName = user.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = user.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = user.Email ?? userToUpdate.Email;
            userToUpdate.Password = user.Password ?? userToUpdate.Password;
            userToUpdate.PhoneNumber = user.PhoneNumber ?? userToUpdate.PhoneNumber;
            forumDb.SaveChanges();
            return userToUpdate;
        }


        public bool EmailExist(string email)
        {
            bool result = forumDb.Users.Any(u => u.Email.ToLower() == email.ToLower());
            return result;
        }
        public bool UsernameExist(string username)
        {
            bool result = forumDb.Users.Any(u => u.Username.ToLower() == username.ToLower());
            return result;
        }

        public bool DeleteUser(User user)
        {
            foreach (var Comment in user.Comments)
            {
                Comment.DeletedOn = DateTime.Now;
                Comment.IsDeleted = true;
            }
            foreach (var Post in user.Posts)
            {
                Post.DeletedOn = DateTime.Now;
                Post.IsDeleted = true;
            }
            user.DeletedOn = DateTime.Now;
            user.IsDeleted = true;
            forumDb.SaveChanges();
            return true;
        }

	}
}
