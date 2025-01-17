using OnlineBank.Web.Endpoints;

namespace OnlineBank.Web.Extensions
{
    public static class RegistrEndpoints
    {
        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapLogInEndpoints();
            return app;
        }
    }
}
