using System.Net;
using CQRS.Application.ErrorHandlers.Base;

namespace CQRS.Application.ErrorHandlers;

public class UnauthorizedException:BaseException
{
    private const int _statusCode = (int)HttpStatusCode.NotFound;
    private const string? _title = "Resource conflict.";

    public UnauthorizedException()
    {
        StatusCode = _statusCode;
        Title = _title;
    }

    public UnauthorizedException(string? message) : base(message)
    {
        StatusCode = _statusCode;
        Title = _title;
    }
}