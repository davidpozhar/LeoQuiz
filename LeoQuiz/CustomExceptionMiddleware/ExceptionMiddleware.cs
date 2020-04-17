using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LeoQuiz.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var errorDetails = new ErrorDetails();

            //Замінити на світч при можливості - пошукати
            if (exception is NullReferenceException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                errorDetails.StatusCode = 404;
                errorDetails.Message = "Not Found.";

            } else if (exception is Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                errorDetails.StatusCode = 500;
                errorDetails.Message = "Internal Server Error.";
            }

            return context.Response.WriteAsync(errorDetails.ToString());


        }
    }
}
