using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Application;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}