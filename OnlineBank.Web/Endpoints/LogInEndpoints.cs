using Newtonsoft.Json.Linq;
using OnlineBank.Application.Abstractions;
using OnlineBank.Core.Models;
using OnlineBank.Infrastructure.Abstractions;
using OnlineBank.Infrastructure.Contracts;
using System.Security.Claims;

namespace OnlineBank.Web.Endpoints
{
    public static class LogInEndpoints
    {
        public static IEndpointRouteBuilder MapLogInEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/login", () =>
            {
                return Results.File("pages/formLogIn.html", "text/html;");
            });
            app.MapGet("/logout", (HttpContext context) =>
            {
                context.Response.Cookies.Delete("jwt");
                return Results.Ok();
            }).RequireAuthorization("OnlyForAuthUser");
            app.MapGet("/registr", () =>
            {
                return Results.File("pages/formRegistration.html", "text/html");
            });
            app.MapGet("/formRegistration.html", () =>
            {
                return Results.File("pages/formRegistration.html", "text/html");
            });
            app.MapGet("/index.html", () =>
            {
                return Results.File("index.html", "text/html");
            }).RequireAuthorization("OnlyForAuthUser");
            app.MapPost("/api/users", async (HttpContext context) =>
            {
                var reader = new StreamReader(context.Request.Body);
                var json = await reader.ReadToEndAsync();
                var dataContext = JObject.Parse(json);
                if (dataContext is not null)
                {
                    string login = dataContext["login"]!.ToString();
                    string password = dataContext["password"]!.ToString();
                    if (login is null || password is null)
                    {
                        return Results.BadRequest("Необходимо ввести данные");
                    }
                    var userServise = context.RequestServices.GetService<IUsersService>();
                    var userPassword = await userServise!.GetPasswordUserAsync(login);
                    if (userPassword is null)
                    {
                        return Results.BadRequest("Пользователь не найден");
                    }
                    var passwordHasher = app.ServiceProvider.GetService<IPasswordHasher>();
                    if (passwordHasher!.Verify(password, userPassword))
                    {
                        var jwtGenerate = context.RequestServices.GetService<IJwtProvider>();
                        var user = await userServise.GetUserAsync(login);
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Role, user!.Role)
                        };
                        var token = jwtGenerate!.GenerateToken(new JwtRequest()
                        {
                            Claims = claims
                        });
                        context.Response.Cookies.Append("jwt", token!);
                        return Results.Redirect("/index.html");
                    }
                    else
                    {
                        return Results.BadRequest("Неверный пароль");
                    }
                }
                return Results.BadRequest("Ошибка передачи данных. Попробуйте еще раз");

            });
            app.MapPost("/api/createUser", async (HttpContext context) =>
            {
                var reader = new StreamReader(context.Request.Body);
                var json = await reader.ReadToEndAsync();
                var dataContext = JObject.Parse(json);
                if (dataContext is not null)
                {
                    string numberCard = dataContext["numberCard"]!.ToString();
                    string dateEnd = dataContext["dateEnd"]!.ToString();
                    string cvv = dataContext["cvv"]!.ToString();
                    string login = dataContext["login"]!.ToString();
                    string password = dataContext["password"]!.ToString();
                    var user = Users.Create(login, password, numberCard, dateEnd, cvv);
                    if (!string.IsNullOrEmpty(user.error))
                    {
                        return Results.BadRequest(user.error);
                    }
                    var userService = context.RequestServices.GetService<IUsersService>();
                    await userService!.CreateNewUserAsync(user.user!);
                    var jwtGenerate = context.RequestServices.GetService<IJwtProvider>();
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, user.user!.Role)
                    };
                    var token = jwtGenerate!.GenerateToken(new JwtRequest()
                    {
                        Claims = claims
                    });
                    context.Response.Cookies.Append("jwt", token!);
                    return Results.Redirect("/index.html");
                }
                return Results.BadRequest("Ошибка передачи данных. Попробуйте еще раз");
            });
            return app;
        }
    }
}
