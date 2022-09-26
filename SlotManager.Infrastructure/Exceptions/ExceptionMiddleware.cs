using Humanizer;
using Microsoft.AspNetCore.Http;
using SlotManager.Core.Exceptions;

namespace SlotManager.Infrastructure.Exceptions
{
    internal sealed class ExceptionMiddleware : IMiddleware
    {
        private record Error(string Code, string Reason);

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.ToString());
                await HandleExceptionAsync(exception, context);
            }
        }

        private async Task HandleExceptionAsync(Exception exception, HttpContext context)
        {
            var(statusCode, error) = exception switch
            {
                CustomException => (StatusCodes.Status400BadRequest, new Error(
                    exception.GetType().Name.Underscore().Replace("_exception", string.Empty),
                    exception.Message)),
                _ => (StatusCodes.Status500InternalServerError, new Error("error", "There was a error."))
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
