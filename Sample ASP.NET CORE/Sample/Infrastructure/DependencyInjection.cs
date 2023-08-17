using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;
using Application.Services;
using Domain.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, string databaseConnection)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            //Add db context
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(databaseConnection));

            //Add Mapper
            services.AddAutoMapper(typeof(UserMappingProfile));

            return services;

        }

    }
}
