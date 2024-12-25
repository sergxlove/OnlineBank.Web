using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace OnlineBank.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseStaticFiles();

            app.Map("/login", () =>
            {
                return Results.File("pages/formLogIn.html", "text/html;");
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
          
            app.Run();
        }
    }
}
