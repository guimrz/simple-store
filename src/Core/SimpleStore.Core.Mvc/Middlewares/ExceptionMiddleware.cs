using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleStore.Core.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SimpleStore.Core.Mvc.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;
            ErrorResponse error = new();

            if (exception is NotFoundException)
            {
                error.Message = exception.Message ?? "The entity could not be found.";
                code = HttpStatusCode.NotFound;
            }
            else if (exception is ValidationException)
            {
                error.Message = "The request model is invalid.";
                error.Content = (exception as ValidationException)?.ValidationResult;
                code = HttpStatusCode.BadRequest;
            }
            else
            {
                code = HttpStatusCode.InternalServerError;
                error.Message = "Internal server error.";

                var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionMiddleware>>();
                logger.LogError(exception, exception.Message);
            }

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!.Equals("development", StringComparison.OrdinalIgnoreCase))
            {
                error.StackTrace = exception.StackTrace;
            }
            var result = JsonConvert.SerializeObject(error, _serializerSettings);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private JsonSerializerSettings _serializerSettings => new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

    }
}