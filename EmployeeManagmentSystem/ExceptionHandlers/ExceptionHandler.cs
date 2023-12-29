using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace EmployeeManagmentSystem.ExceptionHandlers
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong");
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var response = httpContext.Response;
            response.ContentType = MediaTypeNames.Application.Json;
            string errorMessage = exception.Message;

            switch (exception)
            {
                case ArgumentNullException _:
                case ArgumentException _:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case InvalidOperationException _:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case NotSupportedException _:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage = "Internal server error detected.";
                    break;
            }

            var result = JsonSerializer.Serialize(new { response.StatusCode, errorMessage });
            await response.WriteAsync(result);
        }
    }
}
