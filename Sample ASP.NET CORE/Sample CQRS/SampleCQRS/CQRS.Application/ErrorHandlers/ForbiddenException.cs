using System.Net;
using CQRS.Application.ErrorHandlers.Base;

namespace CQRS.Application.ErrorHandlers;

public class ForbiddenException: BaseException
{
    private const int _statusCode = (int)HttpStatusCode.Conflict;
    private const string? _title = "Resource conflict.";

    public ForbiddenException()
    {
        StatusCode = _statusCode;
        Title = _title;
    }

    public ForbiddenException(string? message) : base(message)
    {
        StatusCode = _statusCode;
        Title = _title;
    }
}