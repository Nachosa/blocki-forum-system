using ForumSystem.Business.Abstractions;
using ForumSystem.Business;
using ForumSystem.Api.Controllers;
using ForumSystem.DataAccess;

namespace ForumSystemBusiness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IForumSystemRepository, ForumSystemRepository>();

            var app = builder.Build();
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.MapControllers();
            //app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });


            app.Run();
        }
    }
}