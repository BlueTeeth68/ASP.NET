using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this ServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString: connectionString));

        return services;
    }
}