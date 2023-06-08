using ForumSystem.Business;
using ForumSystem.Api.Controllers;
using ForumSystem.DataAccess;
using ForumSystem.Business.UserService;
using ForumSystem.Business.AutoMapperProfile;
using ForumSystem.Business.CommentService;
using ForumSystem.DataAccess.UserRepo;
using ForumSystem.DataAccess.CommentRepo;
using ForumSystem.DataAccess.PostRepo;

namespace ForumSystemBusiness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IPostRepository, PostRepository>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICommentService, CommentService>();

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
            app.MapControllers();
            //app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

            app.Run();
        }
    }
}