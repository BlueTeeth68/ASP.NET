using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> LoginAsync(UserLogin userLogin);
    }
}
