namespace CQRS.Persistence.Identity.Entities;

public class UserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public virtual AppUser User { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}