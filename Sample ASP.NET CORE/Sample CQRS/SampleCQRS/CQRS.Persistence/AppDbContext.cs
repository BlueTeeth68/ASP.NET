using CQRS.Domain.Entities;
using CQRS.Persistence.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Persistence;

public class AppDbContext: IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<AppUser> AppUsers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
}