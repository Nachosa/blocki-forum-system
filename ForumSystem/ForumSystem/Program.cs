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