using Application.DTOs;

namespace WebApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new ApiResponse<object>();

            switch (exception)
            {
                case Application.Common.Exceptions.ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Success = false;
                    response.Message = "Validation error";
                    response.Data = validationException.Errors;
                    break;

                case Application.Common.Exceptions.NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Success = false;
                    response.Message = notFoundException.Message;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Success = false;
                    response.Message = "An error occurred while processing your request";
                    break;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}