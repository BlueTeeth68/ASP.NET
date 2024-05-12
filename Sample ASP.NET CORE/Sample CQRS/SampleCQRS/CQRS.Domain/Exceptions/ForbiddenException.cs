using System.Net;
using CQRS.Domain.Exceptions.Base;

namespace CQRS.Domain.Exceptions;

public class ForbiddenException: BaseException
{
    private const int _statusCode = (int)HttpStatusCode.Conflict;
    private const string? _title = "Forbidden.";

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