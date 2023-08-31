using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("author")]
public class Author: BaseEntity
{
    public string Name { get; set; }
    public List<Book> Books { get; } = new();
}