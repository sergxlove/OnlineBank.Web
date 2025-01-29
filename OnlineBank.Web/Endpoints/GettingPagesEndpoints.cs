namespace OnlineBank.Web.Endpoints
{
    public static class GettingPagesEndpoints 
    {
        public static IEndpointRouteBuilder MapGettingPagesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/login", () =>
            {
                return Results.File("pages/formLogIn.html", "text/html;");
            });

            app.MapGet("/registr", () =>
            {
                return Results.File("pages/formRegistration.html", "text/html");
            });

            app.MapGet("/formRegistration.html", () =>
            {
                return Results.File("pages/formRegistration.html", "text/html");
            });

            app.MapGet("newCards.html", () =>
            {
                return Results.File("pages/newCards.html", "text/html");
            });

            app.MapGet("/index.html", () =>
            {
                return Results.File("index.html", "text/html");
            }).RequireAuthorization("OnlyForAuthUser");


            return app;
        }
    }
}
