using System.Net;
using CQRS.Domain.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQRS.Infrastructure.Web.GlobalExceptions;

public class GlobalExceptionsHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionsHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = GetStatusCode(exception);

        var response = new ErrorDetail()
        {
            StatusCode = context.Response.StatusCode,
            Title = GetTitle(exception),
            Message = exception.Message
        };

        // var jsonResponse = JsonCon.SerializeObject(response);
        return context.Response.WriteAsync(response.ToString());
    }

    private static int GetStatusCode(Exception exception)
    {
        // Customize status codes based on exception types
        if (exception is BaseException baseException)
        {
            return baseException.StatusCode;
        }
        else
        {
            return (int)HttpStatusCode.InternalServerError;
        }
    }

    private static string GetTitle(Exception exception)
    {
        // Customize error messages based on exception types
        if (exception is BaseException baseException)
        {
            return baseException?.Title ?? "";
        }
        else
        {
            return "Internal server error.";
        }
    }
}