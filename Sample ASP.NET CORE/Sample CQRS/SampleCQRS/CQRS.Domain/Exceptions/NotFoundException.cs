using System.Net;
using CQRS.Domain.Exceptions.Base;

namespace CQRS.Domain.Exceptions;

public class NotFoundException:BaseException
{
    private const int _statusCode = (int)HttpStatusCode.NotFound;
    private const string? _title = "Not found.";

    public NotFoundException()
    {
        StatusCode = _statusCode;
        Title = _title;
    }

    public NotFoundException(string? message) : base(message)
    {
        StatusCode = _statusCode;
        Title = _title;
    }
}