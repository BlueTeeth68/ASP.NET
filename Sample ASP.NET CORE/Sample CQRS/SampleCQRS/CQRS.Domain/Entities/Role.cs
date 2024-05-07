using System.ComponentModel.DataAnnotations;
using CQRS.Domain.Entities.Base;

namespace CQRS.Domain.Entities;

public class Role:BaseEntity<Guid>, IAggregateRoot
{
    [StringLength(20)] public string Name { get; set; } = null!;


}