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
            var result = forumDb.Users.Where(u => u.IsDeleted == false).ToList();
            return result;
        }

        public User GetUserById(int Id)
        {
            var user = forumDb.Users.Include(p => p.Posts).Include(c => c.Comments).FirstOrDefault(u => u.Id == Id && u.IsDeleted == false);
            return user;
        }

        public User GetUserByUserName(string UserName)
        {
            var userWithThatUserName = forumDb.Users.Include(p => p.Posts).Include(c => c.Comments).FirstOrDefault(u => u.Username == UserName && u.IsDeleted == false);
            return userWithThatUserName;
        }

        public User GetUserByEmail(string email)
        {
            var userWithThatEmail = forumDb.Users.FirstOrDefault(u => u.Email == email);
            return userWithThatEmail;
        }

        public IEnumerable<User> GetUsersByFirstName(string firstName)
        {
            var usersWithThatName = forumDb.Users.Where(u => u.FirstName == firstName);
            return usersWithThatName;
        }

        public int GetUsersCount()
        {
            var activeUsersCount = forumDb.Users.Count(u=>u.IsDeleted==false);
            return activeUsersCount;
            
        }

        public List<User> SearchBy(UserQueryParams queryParams)
        {
            var result = forumDb.Users.Where(u => u.IsDeleted == false).ToList();

            if (queryParams.FirstName is not null)
            {
                result = result.FindAll(u => u.FirstName.Contains(queryParams.FirstName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            if (queryParams.UserName is not null)
            {
                result = result.FindAll(u => u.Username == queryParams.UserName);
            }
            if (queryParams.Email is not null)
            {
                result = result.FindAll(u => u.Email == queryParams.Email);
            }
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



        public bool MakeUserAdmin(User user)
        {
            user.RoleId = 3;
            forumDb.SaveChanges();
            return true;
        }


        public bool EmailExist(string email)
        {
            bool result = forumDb.Users.Any(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
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
