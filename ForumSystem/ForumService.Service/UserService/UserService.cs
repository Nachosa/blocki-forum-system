using AutoMapper;
using ForumSystem.Business.CreateAndUpdate_UserDTO;
using ForumSystem.Business.Helper;
using ForumSystem.DataAccess;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Exceptions;

namespace ForumSystem.Business.UserService
{
    public class UserService : IUserService
    {
        private readonly IForumSystemRepository repo;
        private readonly IMapper userMapper;
        public UserService(IForumSystemRepository repo,IMapper createUserMapper)
        {
            this.repo = repo;
            this.userMapper = createUserMapper;
        }
        public User CreateUser(CreateUserDTO userDTO)
        {
            User mappedUser=userMapper.Map<User>(userDTO);
            return repo.CreateUser(mappedUser);
        }
        public IEnumerable<User> GetAllUsers()
        {
            var allUsers = repo.GetAllUsers();
            bool anyUsers = allUsers.Any();
            if (anyUsers == false)
            {
                throw new Exception("There aren't any users yet!");
            }
            return allUsers;
        }

        public void DeleteUser(int userId)
        {
            var userToDelete = repo.FindUserById(userId);
            if (userToDelete is null)
            {
                throw new Exception($"User with Id={userId} was not found!");
            }
            repo.DeleteUser(userToDelete);
        }

        public User FindUserById(int userId)
        {
            var user = repo.FindUserById(userId);
            return user ?? throw new Exception($"User with Id={userId} was not found!");
        }
        

        public bool UpdateUser(int userId, UpdateUserDTO userDTO)
        {
            var userToUpdate = repo.FindUserById(userId);
            if (userToUpdate is null)
            {
                throw new EntityNotFoundException($"User with id:{userId} was not found!");
            }
            var mappedUser = userMapper.Map<User>(userDTO);
            mappedUser.Id = userId;
            repo.UpdateUser(mappedUser);
            return true;


        }
    }
}
