using System.ComponentModel.DataAnnotations;
using CQRS.Domain.Constant.Enums;
using Microsoft.AspNetCore.Identity;

namespace CQRS.Persistence.Identity.Entities;

public class AppUser : IdentityUser<Guid>
{
    [StringLength(50)] public string? FullName { get; set; }

    public Gender? Gender { get; set; }

    [Timestamp] public byte[] RowVersion { get; set; } = null!;
    public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedDateTime { get; set; }
}