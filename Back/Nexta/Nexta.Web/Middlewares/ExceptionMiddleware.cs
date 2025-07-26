using FluentValidation;
using Nexta.Domain.Exceptions;
using Nexta.Infrastructure.Exceptions;
using Nexta.Web.Models;
using System.Text.Json;

namespace Nexta.Web.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
	
		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_logger = logger;
			_next = next;
		}
		
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Глобальная ошибка");
				await HandleExceptionAsync(context, ex);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			var (statusCode, message) = ex switch
			{
				UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, ex.Message),
				ConflictException => (StatusCodes.Status409Conflict, ex.Message),
				BadRequestException => (StatusCodes.Status400BadRequest, ex.Message),
				NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
				ValidationException => (StatusCodes.Status422UnprocessableEntity, ex.Message),
				MinioFileCreationException => (StatusCodes.Status400BadRequest, ex.Message),
				_ => (StatusCodes.Status502BadGateway, ex.Message)
			};

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = statusCode;

			var errorResponse = new ErrorResponseModel(message, statusCode);

			await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
		}
	}
}
