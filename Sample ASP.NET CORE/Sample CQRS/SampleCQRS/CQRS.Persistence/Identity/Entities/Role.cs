using CQRS.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace CQRS.Persistence.Identity.Entities;

public class Role : IdentityRole<Guid>, IAggregateRoot
{
}