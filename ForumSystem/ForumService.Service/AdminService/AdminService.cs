using AutoMapper;
using ForumSystem.DataAccess.AdminRepo;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.PostRepo;
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
                return adminRepo.MakeUserAdmin(user);
            }
            else if (email is not null)
            {
                var user = userRepo.GetUserByEmail(email);
                if (user is null) throw new EntityNotFoundException($"User with Email:{email} was not found!");
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
                return adminRepo.BlockUser(user);
            }
            else if (email is not null)
            {
                var user = userRepo.GetUserByEmail(email);
                if (user is null) throw new EntityNotFoundException($"User with Email:{email} was not found!");
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
                if(user.RoleId==2) throw new EntityNotBlockedException("User wasn't blocked so you can't ublock!");
                return adminRepo.UnBlockUser(user);
            }
            else if (email is not null)
            {
                var user = userRepo.GetUserByEmail(email);
                if (user is null) throw new EntityNotFoundException($"User with Email:{email} was not found!");
                if (user.RoleId == 2) throw new EntityNotBlockedException("User wasn't blocked so you can't ublock!");
                return adminRepo.UnBlockUser(user);
            }
            else
            {
                throw new ArgumentNullException("Please privide Id or email of the user!");
            }
        }

        public bool DeletePost(int? id)
        {
            if(id is null) new ArgumentNullException("Please privide Id of the post!");
            var post = postRepo.GetPostById((int)id);
            if (post is null)
            {
                throw new EntityNotFoundException($"Post with {id} was not found!");
            }
            return postRepo.DeletePostById((int)id);
            
        }
    }
}
