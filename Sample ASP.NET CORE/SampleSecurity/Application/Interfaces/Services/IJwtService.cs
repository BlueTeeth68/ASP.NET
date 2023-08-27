using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(User user);
        Task<string> GenerateRefreshTokenAsync(User user);


    }
}
