using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Entities;
using LeoQuiz.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace LeoQuiz.Extentions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var errorDetails = new ErrorDetails();

                        if(contextFeature?.Error is NullReferenceException)
                        {
                            errorDetails.StatusCode = 404;
                            errorDetails.Message = contextFeature.Error.Message;
                        }

                        if (contextFeature?.Error is Exception)
                        {
                            errorDetails.StatusCode = 500;
                            errorDetails.Message = errorDetails.Message = contextFeature.Error.Message;
                        }


                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        
                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
