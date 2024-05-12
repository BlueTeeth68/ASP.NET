using System.Net;
using CQRS.Domain.Exceptions.Base;

namespace CQRS.Domain.Exceptions;

public class UnauthorizedException:BaseException
{
    private const int _statusCode = (int)HttpStatusCode.NotFound;
    private const string? _title = "Un authorized.";

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