using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using FluentValidation;

namespace HubtelWallet.API.Middlewares
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) =>
            _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var (statusName, statusCode) = GetStatusCode(exception);

            var response = new
            {
                status = false,
                message = exception.Message,
                errors = GetErrors(exception)
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static (string, int) GetStatusCode(Exception exception) =>
            exception switch
            {
                System.ComponentModel.DataAnnotations.ValidationException or ValidationException => (HttpStatusCode.BadRequest.ToString(), StatusCodes.Status400BadRequest),
                _ => (HttpStatusCode.InternalServerError.ToString(), StatusCodes.Status500InternalServerError)
            };

        private static IReadOnlyDictionary<string, string[]>? GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]>? errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors.GroupBy(
                   x => x.PropertyName,
                   x => x.ErrorMessage,
                   (propertyName, errorMessages) => new
                   {
                       Key = propertyName,
                       Values = errorMessages.Distinct().ToArray()
                   })
               .ToDictionary(x => x.Key, x => x.Values);
            }
            else if (exception is DbUpdateException dbUpdateException)
            {
                var detailMessage = dbUpdateException.InnerException?.Message.Split('\n').
                    FirstOrDefault(x => x.Contains("DETAIL"));

                var detailMessageArray = new string[] { detailMessage ?? "" };

                errors = detailMessageArray.GroupBy(x => x,
                        (name, msg) => new
                        {
                            Key = $"DbUpdateException",
                            Values = msg.ToArray()
                        })
                    .ToDictionary(x => x.Key, x => x.Values);
            }

            return errors;
        }
    }
}
