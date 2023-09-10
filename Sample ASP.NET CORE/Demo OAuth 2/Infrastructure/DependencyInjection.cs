using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Mapper;
using Infrastructure.Services;

namespace Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependency(this IServiceCollection services, string databaseConnection)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtService, JwtService>();

            //Add db context
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(databaseConnection));

            //Add Mapper
            services.AddAutoMapper(typeof(UserMappingProfile));

            return services;
        }

        
    }
}
