using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using OnlineBank.Application.Abstractions;
using OnlineBank.Application.Services;
using OnlineBank.DataAccess;
using OnlineBank.DataAccess.Abstractions;
using OnlineBank.DataAccess.Repositories;
using OnlineBank.Infrastructure;
using OnlineBank.Infrastructure.Abstractions;
using System.Security.Claims;
using System.Text;

namespace OnlineBank.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DbContextSqlite>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    IConfigurationSection? jwtSettings = builder.Configuration.GetSection("JwtSettings");
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = jwtSettings["Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
                        ValidateIssuerSigningKey = true
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["jwt"];
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyForAdmin", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "admin");
                });
                options.AddPolicy("OnlyForAuthUser", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "user");
                });
            });

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.Map("/login", () =>
            {
                return Results.File("pages/formLogIn.html", "text/html;");
            });
            app.Map("/logout", (HttpContext context) =>
            {
                context.Response.Cookies.Delete("jwt");
                return Results.Ok();
            });
            app.Map("/registr", () =>
            {
                return Results.File("pages/formRegistration.html", "text/html");
            });
            app.Map("/formRegistration.html", () =>
            {
                return Results.File("pages/formRegistration.html", "text/html");
            });
            app.MapPost("/api/users", async (HttpContext context) =>
            {
                app.Logger.LogInformation("done");
                var reader = new StreamReader(context.Request.Body);
                var json = await reader.ReadToEndAsync();
                var dataContext = JObject.Parse(json);
                if (dataContext is not null)
                {
                    string login = dataContext["login"]!.ToString();
                    string password = dataContext["password"]!.ToString();
                    if(login is not null &&  password is not null)
                    {
                        return Results.Ok();
                    }
                }
                return Results.BadRequest(); 
            });
            app.MapPost("/api/createUser", async (HttpContext context) =>
            {
                var reader = new StreamReader(context.Request.Body);
                var json = await reader.ReadToEndAsync();
                var dataContext = JObject.Parse(json);
                if(dataContext is not null)
                {
                    string numberCard = dataContext["numberCard"]!.ToString();
                    string dateEnd = dataContext["dateEnd"]!.ToString();
                    string cvv = dataContext["cvv"]!.ToString();
                    string login = dataContext["login"]!.ToString();
                    string password = dataContext["password"]!.ToString();
                    app.Logger.LogInformation($"{numberCard} - {dateEnd} - {cvv} - {login} - {password}");
                }
                return Results.Ok();
            });
          
            app.Run();
        }
    }
}
