﻿using AutoMapper;
using ForumSystem.Business.CreateAndUpdate_UserDTO;
using ForumSystem.DataAccess;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.Business.CreateUpdateGet_UserDTO;
using ForumSystem.DataAccess.UserRepository;

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

        public void DeleteUser(int userId)
        {
            var userToDelete = repo.FindUserById(userId);
            if (userToDelete is null)
            {
                throw new Exception($"User with Id={userId} was not found!");
            }
            repo.DeleteUser(userToDelete);
        }

        public GetUserDTO FindUserById(int userId)
        {
            var originalUser = repo.FindUserById(userId) ?? throw new EntityNotFoundException($"User with Id={userId} was not found!");
            GetUserDTO userDTO = userMapper.Map<GetUserDTO>(originalUser);
            return userDTO;
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
