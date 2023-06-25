using ForumSystem.Business;
using ForumSystem.Api.Controllers;
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

namespace ForumSystemBusiness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
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

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //        //ValidAudience = builder.Configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey
            //        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = false,
            //        ValidateIssuerSigningKey = true
            //    };
            //});
            //builder.Services.AddAuthorization();

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

//            app.MapPost("/security/createToken", [AllowAnonymous] (User user) =>
//            {
//                if (user.Username == "greyWolfNinjaMaster__XX69" && user.Password == "123456789pass")
//                {
//                    //var issuer = builder.Configuration["Jwt:Issuer"];
//                    //var audience = builder.Configuration["Jwt:Audience"];
//                    var key = Encoding.ASCII.GetBytes
//                    (builder.Configuration["Jwt:Key"]);
//                    var tokenDescriptor = new SecurityTokenDescriptor
//                    {
//                        Subject = new ClaimsIdentity(new[]
//                        {
//                new Claim("Id", Guid.NewGuid().ToString()),
//                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
//                new Claim(JwtRegisteredClaimNames.Email, user.Username),
//                new Claim(JwtRegisteredClaimNames.Jti,
//                Guid.NewGuid().ToString())
//             }),
//                        Expires = DateTime.UtcNow.AddMinutes(1),
//                        //Issuer = issuer,
//                        //Audience = audience,
//                        SigningCredentials = new SigningCredentials
//                        (new SymmetricSecurityKey(key),
//                        SecurityAlgorithms.HmacSha512Signature)
//                    };
//                    var tokenHandler = new JwtSecurityTokenHandler();
//                    var token = tokenHandler.CreateToken(tokenDescriptor);
//                    var jwtToken = tokenHandler.WriteToken(token);
//                    var stringToken = tokenHandler.WriteToken(token);
//                    return Results.Ok(stringToken);
//                }
//                return Results.Unauthorized();
//            });

//            app.MapGet("/security/getMessage",
//() => "Hello World!").RequireAuthorization();

            app.Run();
        }
    }
}