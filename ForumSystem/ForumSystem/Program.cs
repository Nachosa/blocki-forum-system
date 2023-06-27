using ForumSystem.Business;
using ForumSystem.Api.ApiControllers;
using ForumSystem.DataAccess;
using ForumSystem.Business.UserService;
using ForumSystem.Business.AutoMapperProfile;
using ForumSystem.Business.CommentService;
using ForumSystem.Business.TagService;
using ForumSystem.DataAccess.UserRepo;
using ForumSystem.DataAccess.CommentRepo;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.TagRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ForumSystem.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.DataAccess.AdminRepo;
using ForumSystem.Business.AdminService;

namespace ForumSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<IAuthManager, AuthManager>();
            //builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddDbContext<ForumSystemContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.MapControllers();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
            app.UseStaticFiles();
            app.Run();
        }
    }
}