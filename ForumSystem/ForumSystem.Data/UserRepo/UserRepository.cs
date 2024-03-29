﻿using System;
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
            var result = GetUsers();
            return result.ToList();
        }

        public User GetUserById(int Id)
        {
            var userWithThatId = GetUsers().FirstOrDefault(u => u.Id == Id && u.IsDeleted == false);
            return userWithThatId;
        }

        public User GetUserByUserName(string Username)
        {
            var userWithThatUserName = GetUsers().FirstOrDefault(u => u.Username == Username && u.IsDeleted == false);
            return userWithThatUserName;
        }
        public List<User> GetUsersByUsernameContains(string input)
        {

            var usersWhichUsernameCointainsInput = GetUsers().Where(u => u.Username.ToLower().Contains(input.ToLower()) && u.IsDeleted == false);
            return usersWhichUsernameCointainsInput.ToList();
        }

        public User GetUserByEmail(string email)
        {
            var userWithThatEmail = GetUsers().FirstOrDefault(u => u.Email == email && u.IsDeleted == false);
            return userWithThatEmail;
        }

        public IEnumerable<User> GetUsersByFirstName(string firstName)
        {
            var usersWithThatName = GetUsers().Where(u => u.FirstName == firstName && u.IsDeleted == false);
            return usersWithThatName;
        }

        public int GetUsersCount()
        {
            var activeUsersCount = forumDb.Users.Count(u => u.IsDeleted == false);
            return activeUsersCount;

        }

        public List<User> SearchBy(UserQueryParams queryParams)
        {
            var query = GetUsers();


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
            var userToUpdate = forumDb.Users.FirstOrDefault(u => u.Username == userName);
            userToUpdate.FirstName = user.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = user.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = user.Email ?? userToUpdate.Email;
            userToUpdate.Password = user.Password ?? userToUpdate.Password;
			userToUpdate.PhoneNumber = user.PhoneNumber ?? userToUpdate.PhoneNumber;
			userToUpdate.ProfilePicPath = user.ProfilePicPath ?? userToUpdate.ProfilePicPath;
			if (user.ProfilePicPath == "Delete") { userToUpdate.ProfilePicPath = null; }
			forumDb.SaveChanges();
            return userToUpdate;
        }
        public User UpdateUser(int id, User user)
        {
            var userToUpdate = forumDb.Users.FirstOrDefault(u => u.Id == id);
            userToUpdate.FirstName = user.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = user.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = user.Email ?? userToUpdate.Email;
            userToUpdate.Password = user.Password ?? userToUpdate.Password;
            userToUpdate.PhoneNumber = user.PhoneNumber ?? userToUpdate.PhoneNumber;
            userToUpdate.ProfilePicPath = user.ProfilePicPath ?? userToUpdate.ProfilePicPath;
            if (user.ProfilePicPath == "Delete") { userToUpdate.ProfilePicPath =null; }
            forumDb.SaveChanges();
            return userToUpdate;
        }
        private void Update(User userToUpdate,User userNewValues)
        {
			userToUpdate.FirstName = userNewValues.FirstName ?? userToUpdate.FirstName;
			userToUpdate.LastName = userNewValues.LastName ?? userToUpdate.LastName;
			userToUpdate.Email = userNewValues.Email ?? userToUpdate.Email;
			userToUpdate.Password = userNewValues.Password ?? userToUpdate.Password;
			userToUpdate.PhoneNumber = userNewValues.PhoneNumber ?? userToUpdate.PhoneNumber;
			userToUpdate.ProfilePicPath = userNewValues.ProfilePicPath ?? userToUpdate.ProfilePicPath;
			forumDb.SaveChanges();
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

        private IQueryable<User> GetUsers()
        {
            var users = forumDb.Users
                                    .Include(u => u.Posts.Where(p => p.IsDeleted == false))
                                        .ThenInclude(p => p.Tags.Where(pt => pt.Tag.IsDeleted == false))
                                            .ThenInclude(pt => pt.Tag)
                                    .Include(u => u.Posts.Where(p => p.IsDeleted == false))
                                        .ThenInclude(p => p.Likes.Where(l => l.IsDeleted == false))
                                    .Include(u => u.Comments.Where(c => c.IsDeleted == false))
                                        .ThenInclude(c => c.Likes.Where(l => l.IsDeleted == false));
            return users;
        }
    }
}
