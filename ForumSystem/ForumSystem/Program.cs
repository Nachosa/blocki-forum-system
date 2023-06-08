using ForumSystem.Business;
using ForumSystem.Api.Controllers;
using ForumSystem.DataAccess;
using ForumSystem.DataAccess.ReposContracts;
using ForumSystem.DataAccess.Repos;
using ForumSystem.DataAccess.Helpers;
using ForumSystem.Business.UserService;
using ForumSystem.Business.Helper;
using ForumSystem.Business.AutoMapperProfile;

namespace ForumSystemBusiness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IPostRepository, PostRepository>();
            builder.Services.AddSingleton<IForumSystemRepository, ForumSystemRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<CreateUserMapper>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            builder.Services.AddScoped<PostMapper>();

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
            app.MapControllers();
            //app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

            app.Run();
        }
    }
}