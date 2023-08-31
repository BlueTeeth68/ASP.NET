using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table(("book"))]
public class Book : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime DateOfPublication { get; set; }
    public string Publisher { get; set; }
    public List<Author> Authors { get; } = new();
}