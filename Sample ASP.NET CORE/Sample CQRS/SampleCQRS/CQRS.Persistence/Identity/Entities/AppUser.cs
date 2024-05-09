using System.ComponentModel.DataAnnotations;
using CQRS.Domain.Constant.Enums;
using CQRS.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace CQRS.Persistence.Identity.Entities;

public class AppUser :  IdentityUser<Guid>,  IAggregateRoot
{
    [StringLength(50)] public string FullName { get; set; } = null!;

    public Gender? Gender { get; set; }

    [Timestamp] public byte[] RowVersion { get; set; } = null!;
    public DateTimeOffset CreatedDateTime { get; set; }
    public DateTimeOffset? UpdatedDateTime { get; set; }
}