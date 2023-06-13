﻿using AutoMapper;
using ForumSystem.DataAccess.AdminRepo;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepo;
        private readonly IUserRepository userRepo;
        private readonly IMapper userMapper;
        public AdminService(IUserRepository userRepo, IMapper createUserMapper,IAdminRepository adminRepo)
        {
            this.adminRepo = adminRepo;
            this.userRepo = userRepo;
            this.userMapper = createUserMapper;
        }


        public void MakeUserAdmin(int? id, string email)
        {
            if (id is not null)
            {
                var user = userRepo.GetUserById((int)id);
                if (user is null) throw new EntityNotFoundException($"User with Id:{id} was not found!");
                adminRepo.MakeUserAdmin(user);
            }
            else if (email is not null)
            {
                var user = userRepo.GetUserByEmail(email);
                if (user is null) throw new EntityNotFoundException($"User with Email:{email} was not found!");
                adminRepo.MakeUserAdmin(user);
            }
            else
            {
                throw new ArgumentNullException("Please privide Id or email of the user!");
            }
        }
        public void BlockUser(int? id, string email)
        {
            if (id is not null)
            {
                var user = userRepo.GetUserById((int)id);
                if (user is null) throw new EntityNotFoundException($"User with Id:{id} was not found!");
                adminRepo.BlockUser(user);
            }
            else if (email is not null)
            {
                var user = userRepo.GetUserByEmail(email);
                if (user is null) throw new EntityNotFoundException($"User with Email:{email} was not found!");
                adminRepo.BlockUser(user);
            }
            else
            {
                throw new ArgumentNullException("Please privide Id or email of the user!");
            }
        }

        public void UnBlockUser(int? id,string email)
        {
            if (id is not null)
            {
                var user = userRepo.GetUserById((int)id);
                if (user is null) throw new EntityNotFoundException($"User with Id:{id} was not found!");
                if(user.RoleId==2) throw new EntityNotBlockedException("User wasn't blocked so you can't ublock!");
                adminRepo.UnBlockUser(user);
            }
            else if (email is not null)
            {
                var user = userRepo.GetUserByEmail(email);
                if (user is null) throw new EntityNotFoundException($"User with Email:{email} was not found!");
                if (user.RoleId == 2) throw new EntityNotBlockedException("User wasn't blocked so you can't ublock!");
                adminRepo.UnBlockUser(user);
            }
            else
            {
                throw new ArgumentNullException("Please privide Id or email of the user!");
            }
        }
    
    }
}
