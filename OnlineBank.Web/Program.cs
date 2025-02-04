using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnlineBank.Application.Abstractions;
using OnlineBank.Application.Services;
using OnlineBank.DataAccess;
using OnlineBank.DataAccess.Abstractions;
using OnlineBank.DataAccess.Repositories;
using OnlineBank.Infrastructure;
using OnlineBank.Infrastructure.Abstractions;
using OnlineBank.Web.Extensions;
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
            builder.Services.AddScoped<ISystemTableRepository, SystemTableRepository>();
            builder.Services.AddScoped<ISystemTableService, SystemTableService>();
            builder.Services.AddScoped<ICardsRepository, CardsRepository>();
            builder.Services.AddScoped<ICardsService, CardsService>();
            builder.Services.AddScoped<IDataUsersRepository, DataUsersRepository>();
            builder.Services.AddScoped<IDataUsersService, DataUsersService>();
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    IConfigurationSection? jwtSettings = builder.Configuration.GetSection("JwtSettings");
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidateAudience = false,
                        ValidAudience = jwtSettings["Audience"],
                        ValidateLifetime = false,
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
            app.MapGet("/", () =>
            {
                return Results.File("pages/formLogIn.html", "text/html;");
            });

            app.MapAllEndpoints();
            
            app.Run();
        }
    }
}
