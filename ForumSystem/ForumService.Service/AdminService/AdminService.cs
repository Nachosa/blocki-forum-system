﻿using AutoMapper;
using ForumSystem.DataAccess.AdminRepo;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.UserRepo;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IPostRepository postRepo;
        private readonly IMapper userMapper;
        public AdminService(IUserRepository userRepo,IPostRepository postRepo, IMapper createUserMapper,IAdminRepository adminRepo)
        {
            this.adminRepo = adminRepo;
            this.userRepo = userRepo;
            this.userMapper = createUserMapper;
            this.postRepo = postRepo;
        }


        public bool MakeUserAdmin(int? id, string email)
        {
            if (id is not null)
            {
                var user = userRepo.GetUserById((int)id);
                if (user is null) throw new EntityNotFoundException($"User with Id:{id} was not found!");
                if (user.RoleId == 3) throw new EntityAlreadyAdminException($"User with Id:{id} is already ADMIN!");
                return adminRepo.MakeUserAdmin(user);
            }
            else if (!email.IsNullOrEmpty())
            {
                var user = userRepo.GetUserByEmail(email);
                if (user is null) throw new EntityNotFoundException($"User with Email:{email} was not found!");
                if (user.RoleId == 3) throw new EntityAlreadyAdminException($"User with Id:{id} is already ADMIN!");
                return adminRepo.MakeUserAdmin(user);
            }
            else
            {
                throw new ArgumentNullException("Please privide Id or email of the user!");
            }
        }
        public bool BlockUser(int? id, string email)
        {
            if (id is not null)
            {
                var user = userRepo.GetUserById((int)id);
                if (user is null) throw new EntityNotFoundException($"User with Id:{id} was not found!");
                if (user.RoleId == 1) throw new EntityAlreadyBlockedException($"User with Id:{id} is already BLOCKED!");
                return adminRepo.BlockUser(user);
            }
            else if (!email.IsNullOrEmpty())
            {
                var user = userRepo.GetUserByEmail(email);
                if (user is null) throw new EntityNotFoundException($"User with Email:{email} was not found!");
                if (user.RoleId == 1) throw new EntityAlreadyBlockedException($"User with Email:{email} is already BLOCKED!");
                return adminRepo.BlockUser(user);
            }
            else
            {
                throw new ArgumentNullException("Please privide Id or email of the user!");
            }
        }

        public bool UnBlockUser(int? id,string email)
        {
            if (id is not null)
            {
                var user = userRepo.GetUserById((int)id);
                if (user is null) throw new EntityNotFoundException($"User with Id:{id} was not found!");
                if(user.RoleId==2) throw new EntityAlreadyUnBlockedException($"User with Id:{id} is already UNBLOCKED!");
                return adminRepo.UnBlockUser(user);
            }
            else if (!email.IsNullOrEmpty())
            {
                var user = userRepo.GetUserByEmail(email);
                if (user is null) throw new EntityNotFoundException($"User with Email:{email} was not found!");
                if (user.RoleId == 2) throw new EntityAlreadyUnBlockedException($"User with Email:{email} is already UNBLOCKED!");
                return adminRepo.UnBlockUser(user);
            }
            else
            {
                throw new ArgumentNullException("Please privide Id or email of the user!");
            }
        }

        public bool DeletePost(int? id)
        {
            if(id is null) throw new ArgumentNullException("Please privide Id of the post!");
            var post = postRepo.GetPostById((int)id);
            if (post is null)
            {
                throw new EntityNotFoundException($"Post with {id} was not found!");
            }
            return postRepo.DeletePostById((int)id);
            
        }
    }
}
