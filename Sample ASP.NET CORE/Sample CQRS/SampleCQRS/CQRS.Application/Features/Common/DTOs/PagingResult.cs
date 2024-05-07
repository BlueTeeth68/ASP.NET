namespace CQRS.Application.Features.Common.DTOs;

public class PagingResult<T>
{
    public int TotalPages { get; set; }

    public long TotalItems { get; set; }

    public List<T> Items { get; set; } = new List<T>();
}