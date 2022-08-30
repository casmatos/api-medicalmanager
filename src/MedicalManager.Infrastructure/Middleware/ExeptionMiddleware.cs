using MedicalManager.Domain.Middlewares;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Claims;

namespace MedicalManager.Infrastructure.Middleware;

public class ExeptionMiddleware : IMiddleware
{
    private readonly ILogger<ExeptionMiddleware> _logger;

    public ExeptionMiddleware(ILogger<ExeptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception argumentExeption) when (argumentExeption.GetType() == typeof(ArgumentException) ||
                                                 argumentExeption.GetType() == typeof(ArgumentNullException))
        {
            await BuildException(argumentExeption, context, HttpStatusCode.BadRequest);
        }
        catch (Exception exception)
        {
            await BuildException(exception, context);
        }
    }

    internal async Task BuildException(Exception exception, HttpContext context, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        if (exception.InnerException is not null)
        {
            while (exception.InnerException is not null)
            {
                exception = exception.InnerException;
            }
        }
        if (context is not null)
        {
            if (context.Response.StatusCode is (int)HttpStatusCode.OK)
            {
                context.Response.StatusCode = (int)statusCode;
            }

            var user = context.User.FindFirst(ClaimTypes.Name) is null ? "Anonymous" : context.User.FindFirst(ClaimTypes.Name)?.Value;

            _logger.LogError(
                $"{exception.Message}{Environment.NewLine}HTTP Request Information:{Environment.NewLine}" +
                $"  Request By: {user}{Environment.NewLine}" +
                $"  RemoteIP: {context.Connection.RemoteIpAddress}{Environment.NewLine}" +
                $"  Schema: {context.Request.Scheme}{Environment.NewLine}" +
                $"  Host: {context.Request.Host}{Environment.NewLine}" +
                $"  Path: {context.Request.Path}{Environment.NewLine}" +
                $"  Query String: {context.Request.QueryString}{Environment.NewLine}" +
                $"  Response Status Code: {context.Response?.StatusCode}{Environment.NewLine}");

            context.Response.ContentType = "application/json";

            var errorResult = new ErrorResult
            {
                StatusCode = context.Response.StatusCode,
                Error = exception.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResult));

        }
    }
}
