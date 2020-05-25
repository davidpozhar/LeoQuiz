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

            switch (exception)
            {
                case NullReferenceException e:
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                        errorDetails.StatusCode = 404;
                        errorDetails.Message = exception.Message;

                        break;
                    }
                case FormatException ex:
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        errorDetails.StatusCode = 400;
                        errorDetails.Message = exception.Message;

                        break;
                    }

                case Exception e:
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        errorDetails.StatusCode = 500;
                        errorDetails.Message = exception.Message;

                        break;
                    }
            }

            return context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
