using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace eCommerce.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddlewares> _log;

        public ExceptionHandlingMiddlewares(RequestDelegate next, ILogger<ExceptionHandlingMiddlewares> log)
        {
            _next = next;
            _log = log;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);

            } catch(Exception ex)
            {
                // log
                _log.LogError($"{ex.GetType().ToString()}: {ex.Message}");
                throw;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewaresExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddlewares(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddlewares>();
        }
    }
}
