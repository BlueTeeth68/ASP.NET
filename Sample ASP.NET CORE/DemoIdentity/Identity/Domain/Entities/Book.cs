namespace Domain.Entities;

public class Book
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? AuthorName { get; set; } = null!;

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }
}