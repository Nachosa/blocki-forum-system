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

namespace ForumSystem.Business.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repo;
        private readonly IMapper userMapper;
        public UserService(IUserRepository repo, IMapper createUserMapper)
        {
            this.repo = repo;
            this.userMapper = createUserMapper;
        }
        public User CreateUser(CreateUserDTO userDTO)
        {
            if (userDTO.Email != null)
            {
                if (repo.EmailExist(userDTO.Email))
                {
                    throw new EmailAlreadyExistException("Email already exist!");
                }
            }

            string decodePassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(userDTO.Password));
            userDTO.Password = decodePassword;

            User mappedUser = userMapper.Map<User>(userDTO);
            return repo.CreateUser(mappedUser);
        }
        public User GetUserById(int userId)
        {
            var originalUser = repo.GetUserById(userId) ?? throw new EntityNotFoundException($"User with Id={userId} was not found!");
            return originalUser;
        }
        public List<User> SearchBy(UserQueryParams queryParams)
        {
            var originalUsers = repo.Searchby(queryParams);

            if (originalUsers.Count == 0)
            {
                throw new EntityNotFoundException($"User not found!");
            }
            return originalUsers;
            
        }
        public IEnumerable<User> GetAllUsers()
        {

            var allUsers = repo.GetAllUsers();

            if (!allUsers.Any())
            {
                throw new EntityNotFoundException("There aren't any users yet!");
            }

            return allUsers;
        }
        public User GetUserByUserName(string userName)
        {
            var user = repo.GetUserByUserName(userName);
            if (user is null)
            {
                throw new EntityNotFoundException($"User with id:{userName} was not found!");
            }
            return user;
        }
        public User UpdateUser(string userName, User userNewValues)
        {
            if (userNewValues.Email != null )
            {
                if (repo.EmailExist(userNewValues.Email))
                {
                    throw new EmailAlreadyExistException("Email already exist!");
                } 
            }

            var userToUpdate = repo.GetUserByUserName(userName);
            if (userToUpdate is null)
            {
                throw new EntityNotFoundException($"User with username:{userName} was not found!");
            }
           // var mappedUser = userMapper.Map<User>(userDTO);
            var updatedUser = repo.UpdateUser(userName,userNewValues);
            return updatedUser;


        }

        public bool DeleteUser(string userName)
        {
            var userToDelete = repo.GetUserByUserName(userName);
            if (userToDelete is null)
            {
                throw new EntityNotFoundException($"User with Id={userName} was not found!");
            }
            return repo.DeleteUser(userToDelete);
        }


    }
}
