using Newtonsoft.Json.Linq;
using OnlineBank.Application.Abstractions;
using OnlineBank.Core.Models;
using OnlineBank.DataAccess.Contracts.Requests;
using OnlineBank.Infrastructure.Abstractions;
using OnlineBank.Infrastructure.Contracts;
using System.Security.Claims;

namespace OnlineBank.Web.Endpoints
{
    public static class LogInEndpoints
    {

        public static IEndpointRouteBuilder MapLogInEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/logout", (HttpContext context) =>
            {
                context.Response.Cookies.Delete("jwt");
                return Results.Ok();
            }).RequireAuthorization("OnlyForAuthUser");

            app.MapPost("/api/users", async (HttpContext context) =>
            {
                try
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
                        var passwordHasher = app.ServiceProvider.GetService<IPasswordHasherService>();
                        if (passwordHasher!.VerifyUser(password, userPassword))
                        {
                            var jwtGenerate = context.RequestServices.GetService<IJwtProvider>();
                            var user = new Users(await userServise.GetUserAsync(login) 
                                ?? throw new Exception("Произошла ошибка"));
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
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return Results.Text(ex.Message);
                }

            });
            app.MapPost("/api/restoreUser", async (HttpContext context) =>
            {
                try
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
                        bool checkLogin = (bool)dataContext["checkLogin"]!;
                        var passwordHasherService = app.ServiceProvider.GetService<IPasswordHasherService>();
                        var user = Users.Create(login, password, passwordHasherService!.GenerateHashPassword);
                        if (!string.IsNullOrEmpty(user.error))
                        {
                            return Results.BadRequest(user.error);
                        }
                        if (checkLogin is true)
                        {
                            var cardService = context.RequestServices.GetService<ICardsService>();
                            Guid? userId = await cardService!.VerifyCard(numberCard, dateEnd, cvv);
                            if(userId is null)
                            {
                                return Results.BadRequest("Не удалось найти пользователя");
                            }
                            var userService = context.RequestServices.GetService<IUsersService>();
                            await userService!.UpdateLoginAndPasswordAsync(userId.Value, login, password);
                            var jwtGenerate = context.RequestServices.GetService<IJwtProvider>();
                            var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Role, await userService.GetRoleUserAsync(login))
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
                            var cardService = context.RequestServices.GetService<ICardsService>();
                            Guid? userId = await cardService!.VerifyCard(numberCard, dateEnd, cvv);
                            if (userId is null)
                            {
                                return Results.BadRequest("Не удалось найти пользователя");
                            }
                            var userService = context.RequestServices.GetService<IUsersService>();
                            if (login != await userService!.GetLoginUserAsync(userId))
                            {
                                return Results.BadRequest("Логины не совпадают");
                            }
                            await userService.UpdatePasswordUserAsync(login, password);
                            var jwtGenerate = context.RequestServices.GetService<IJwtProvider>();
                            var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Role, await userService.GetRoleUserAsync(login))
                            };
                            var token = jwtGenerate!.GenerateToken(new JwtRequest()
                            {
                                Claims = claims
                            });
                            context.Response.Cookies.Append("jwt", token!);
                            return Results.Redirect("/index.html");
                        }
                    }
                    return Results.BadRequest("Ошибка передачи данных. Попробуйте еще раз");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return Results.Text(ex.Message);
                }
            });
            app.MapPost("/api/createCard", async (HttpContext context) =>
            {
                try
                {
                    var reader = new StreamReader(context.Request.Body);
                    var bodyString = await reader.ReadToEndAsync();
                    var json = JObject.Parse(bodyString);
                    if (json is not null)
                    {
                        string firstName = json["firstName"]!.ToString();
                        string secondName = json["secondName"]!.ToString();
                        string lastName = json["lastName"]!.ToString();
                        string dateBirth = json["dateBirth"]!.ToString();
                        string passportData = json["passportData"]!.ToString();
                        string numberPhone = json["numberPhone"]!.ToString();
                        string email = json["email"]!.ToString();
                        string login = json["loginUser"]!.ToString();
                        string password = json["passwordUser"]!.ToString();
                        var userData = DataUsers.Create(firstName, secondName, lastName, dateBirth, passportData, numberPhone, email);
                        if (!string.IsNullOrEmpty(userData.error))
                        {
                            return Results.BadRequest(userData.error);
                        }
                        var passwordHasherService = app.ServiceProvider.GetService<IPasswordHasherService>();
                        var user = Users.Create(login, password, passwordHasherService!.GenerateHashPassword);
                        if (!string.IsNullOrEmpty(user.error))
                        {
                            return Results.BadRequest(user.error);
                        }
                        var cardService = context.RequestServices.GetService<ICardsService>();
                        var dataUserService = context.RequestServices.GetService<IDataUsersService>();
                        var userService = context.RequestServices.GetService<IUsersService>();
                        if(await userService!.CheckHaveUserAsync(login) is true)
                        {
                            return Results.BadRequest("Пользователь с таким логином уже существует");
                        }

                        await userService!.CreateNewUserAsync(user.user!);
                        RequestDataUsers request = new()
                        {
                            Id = user.user!.Id,
                            FirstName = firstName,
                            SecondName = secondName,
                            LastName = lastName,
                            DateBirth = dateBirth,
                            PassportData = passportData,
                            NumberPhone = numberPhone,
                            Email = email
                        };
                        await dataUserService!.AddNewDataUserAsync(request);
                        await cardService!.AddNewCard(await cardService.GenerateNumberCard(),
                            cardService.GenerateDateEnd(), cardService.GenerateCvv(), user.user!.Id);
                        var jwtGenerate = context.RequestServices.GetService<IJwtProvider>();
                        var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Role, await userService.GetRoleUserAsync(login))
                            };
                        var token = jwtGenerate!.GenerateToken(new JwtRequest()
                        {
                            Claims = claims
                        });
                        context.Response.Cookies.Append("jwt", token!);
                        return Results.Redirect("/index.html");
                    }
                    return Results.BadRequest("Ошибка передачи данных. Попробуйте еще раз");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    return Results.Text(ex.Message);
                }
            });
            return app;
        }
    }
}
