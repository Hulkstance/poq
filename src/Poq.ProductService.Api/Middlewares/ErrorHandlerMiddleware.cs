using System.Net;
using System.Text.Json;
using FluentValidation;
using Poq.ProductService.Application.Models;

namespace Poq.ProductService.Api.Middlewares;

internal sealed class ErrorHandlerMiddleware
{
    private readonly bool _isDevelopment;
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
        _isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            if (_isDevelopment)
            {
                var result = JsonSerializer.Serialize(new ResponseBuilder()
                    .WithMessage(error.Message)
                    .Build());

                await response.WriteAsync(result);
            }
            else
            {
                var result = JsonSerializer.Serialize(new ResponseBuilder()
                    .WithMessage(error.Message)
                    .Build());

                await response.WriteAsync(result);
            }
        }
    }
}
