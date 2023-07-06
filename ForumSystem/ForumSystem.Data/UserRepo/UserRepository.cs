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
            //        var user = forumDb.Users.Include(p => p.Posts.Where(p => p.IsDeleted == false)).ThenInclude(p => p.Tags.Where(pt => pt.Tag.IsDeleted == false))
            //.Include(c => c.Comments).Where(c => c.IsDeleted == false)
            //.FirstOrDefault(u => u.Id == Id && u.IsDeleted == false);
            //TODO: Gonna need to add more includes here and in the other get methods probably.
            //In order to display tags, likes and so forth, i have begun writing that above.
            var user = forumDb.Users.Include(p => p.Posts.Where(p => p.IsDeleted == false)).Include(c => c.Comments.Where(c => c.IsDeleted == false)).FirstOrDefault(u => u.Id == Id && u.IsDeleted == false);
            return user;
        }

        public User GetUserByUserName(string UserName)
        {
            var userWithThatUserName = forumDb.Users.Include(p => p.Posts.Where(p => p.IsDeleted == false)).Include(c => c.Comments.Where(c => c.IsDeleted == false)).AsNoTracking().FirstOrDefault(u => u.Username == UserName && u.IsDeleted == false);
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
            var activeUsersCount = forumDb.Users.Count(u => u.IsDeleted == false);
            return activeUsersCount;

        }

        public List<User> SearchBy(UserQueryParams queryParams)
        {
            var query = forumDb.Users
                .Include(u => u.Posts)
                .Include(u => u.Comments)
                .Where(u => u.IsDeleted == false);

            if (queryParams.FirstName is not null)
            {
                query = query.Where(u => u.FirstName.ToLower() == queryParams.FirstName.ToLower());
            }

            if (queryParams.UserName is not null)
            {
                query = query.Where(u => u.Username == queryParams.UserName);
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
