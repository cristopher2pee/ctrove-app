using CTrove.Api.Middleware;
namespace CTrove.Api.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder CustomErrorHandler(this IApplicationBuilder app) 
            => app.UseMiddleware<CustomErrorHandling>();
    }
}
