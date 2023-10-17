using Gateway.Core.Models;
using System.Net;

namespace Gateway.Core.Middlwares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                await HandleExceptionAsync(httpContext,
                    ex.Message,
                    HttpStatusCode.InternalServerError,
                    "Internal Server Error");
            }
        }

        public async Task HandleExceptionAsync(HttpContext httpContext, string exceptionMessage, HttpStatusCode statusCode, string message)
        {
            _logger.LogError(exceptionMessage);

            HttpResponse response = httpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            ErrorDto error = new()
            {
                Message = message,
                StatusCode = response.StatusCode
            };

            string result = error.ToString();
            await response.WriteAsJsonAsync(result);
        }
    }
}
