using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Authorization
{
    public static IServiceCollection AddPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => { policy.RequireRole(Role.Admin.ToString()); });
            options.AddPolicy("Manager", policy => { policy.RequireRole(Role.Manager.ToString()); });
            options.AddPolicy("ManagerOrAdmin",
                policy => { policy.RequireRole(Role.Manager.ToString(), Role.Admin.ToString()); });
        });
        return services;
    }
}