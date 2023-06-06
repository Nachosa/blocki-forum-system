using ForumSystem.Business;
using ForumSystem.Api.Controllers;
using ForumSystem.DataAccess;
using ForumSystem.DataAccess.Helpers;

namespace ForumSystemBusiness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddSingleton<IForumSystemRepository, ForumSystemRepository>();
            builder.Services.AddScoped<PostMapper>();

            var app = builder.Build();
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.MapControllers();
            //app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

            app.Run();
        }
    }
}