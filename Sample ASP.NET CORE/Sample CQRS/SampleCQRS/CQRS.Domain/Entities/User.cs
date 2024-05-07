using System.ComponentModel.DataAnnotations;
using CQRS.Domain.Entities.Base;

namespace CQRS.Domain.Entities;

public class User : BaseEntity<Guid>, IAggregateRoot
{
    [StringLength(50)] public string FullName { get; set; } = null!;

    [StringLength(20)] public string Username { get; set; } = null!;
}