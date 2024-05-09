using System.ComponentModel.DataAnnotations;
using CQRS.Domain.Entities.Base;

namespace CQRS.Domain.Entities;

public class Product : BaseEntity<Guid>, IAggregateRoot
{
    [StringLength(100)] public string Name { get; set; } = null!;

    [StringLength(500)] public string? PictureUrl { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}