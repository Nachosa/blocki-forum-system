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
            User mappedUser = userMapper.Map<User>(userDTO);
            return repo.CreateUser(mappedUser);
        }
        public IEnumerable<GetUserDTO> GetAllUsers()
        {

            var allUsers = repo.GetAllUsers();

            if (!allUsers.Any())
            {
                throw new EntityNotFoundException("There aren't any users yet!");
            }

            return allUsers.Select(currentUser => userMapper.Map<GetUserDTO>(currentUser));
        }
        public List<GetUserDTO> SearchBy(UserQueryParams queryParams)
        {
            var originalUsers = repo.Searchby(queryParams);

            if (originalUsers.Count == 0)
            {
                throw new EntityNotFoundException($"User not found!");
            }
            return originalUsers.Select(u => userMapper.Map<GetUserDTO>(u)).ToList();
            
        }

        public bool DeleteUser(int userId)
        {
            var userToDelete = repo.GetUserById(userId);
            if (userToDelete is null)
            {
                throw new Exception($"User with Id={userId} was not found!");
            }
            return repo.DeleteUser(userToDelete);
        }

        public GetUserDTO GetUserById(int userId)
        {
            var originalUser = repo.GetUserById(userId) ?? throw new EntityNotFoundException($"User with Id={userId} was not found!");
            GetUserDTO userDTO = userMapper.Map<GetUserDTO>(originalUser);
            return userDTO;
        }


        public GetUserDTO UpdateUser(int userId, UpdateUserDTO userDTO)
        {
            var userToUpdate = repo.GetUserById(userId);
            if (userToUpdate is null)
            {
                throw new EntityNotFoundException($"User with id:{userId} was not found!");
            }
            var mappedUser = userMapper.Map<User>(userDTO);
            mappedUser.Id = userId;
            var updatedUser = repo.UpdateUser(mappedUser);
            GetUserDTO updatedUserDTO = userMapper.Map<GetUserDTO>(updatedUser);
            return updatedUserDTO;


        }


    }
}
