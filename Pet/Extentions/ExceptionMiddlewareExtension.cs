using Cinema.Middleware;

namespace Cinema.Extentions
{
    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseMyExceptionMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlerMiddleware>();

            return builder;
        }
    }
}
