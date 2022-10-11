using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.Exceptions;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Api.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.ContentType = Text.Plain;

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    var exception = exceptionHandlerPathFeature?.Error;

                    var logger = loggerFactory.CreateLogger(string.Empty);
                    logger.LogError(exception.StackTrace, exception.Message);

                    switch (exception!.GetType().Name)
                    {
                        case nameof(NoUserFoundException):
                            await WriteNotFoundResponse(context, exception);
                            break;
                        default:
                            await WriteInternalErrorResponse(context, exception);
                            break;
                    }
                });
            });
        }

        private static async Task WriteBadRequestResponse(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(exception.Message);
        }

        private static async Task WriteNotFoundResponse(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(exception.Message);
        }

        private static async Task WriteInternalErrorResponse(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("An exception was thrown. ");
            await context.Response.WriteAsync(string.Concat(exception.InnerException, exception.Message));
        }
    }
}
