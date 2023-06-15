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
        public UserService(IUserRepository userRepo, IMapper createUserMapper,IPostRepository postRepo)
        {
            this.userRepo = userRepo;
            this.userMapper = createUserMapper;
            this.postRepo = postRepo;
        }
        public User CreateUser(CreateUserDTO userDTO)
        {
            if (userRepo.EmailExist(userDTO.Email))
            {
                throw new EmailAlreadyExistException("Email already exist!");
            }

            string decodePassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(userDTO.Password));
            userDTO.Password = decodePassword;

            User mappedUser = userMapper.Map<User>(userDTO);
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
            var user = userRepo.GetUserByUserName(userName);
            if (user is null)
            {
                throw new EntityNotFoundException($"User with username:{userName} was not found!");
            }
            return user;
        }

        public List<User> SearchBy(UserQueryParams queryParams)
        {
            var originalUsers = userRepo.Searchby(queryParams);

            if (originalUsers.Count == 0)
            {
                throw new EntityNotFoundException($"User not found!");
            }
            return originalUsers;

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
            // var mappedUser = userMapper.Map<User>(userDTO);
            var updatedUser = userRepo.UpdateUser(userName, userNewValues);
            return updatedUser;


        }
        public bool DeleteUser(string userName,int? userId)
        {
            if (userId is not null)
            {
                var userToDelete = userRepo.GetUserById((int)userId);
                if(userToDelete is null) throw new EntityNotFoundException($"User with Id={userId} was not found!");
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

        public ICollection<Post> GetPostsWithTag(string tag1)
        {
            var tag = userRepo.GetTagWithName(tag1);
            if (tag is null) throw new EntityNotFoundException("Tag with name:{tag1} was not found!");
            var posts = userRepo.GetPostsWithTag(tag1);
            if (posts is null|| posts.Count==0) throw new EntityNotFoundException("Posts with tag:{tag1} were not found!");
            return posts;
        }
    }
}
