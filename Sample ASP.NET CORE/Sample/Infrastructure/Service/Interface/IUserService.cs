using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service.Interface
{
    public interface IUserService
    {

        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<int> CreateUser(User user);
        Task DeleteUser(int id);
        Task<int> UpdateUser(User user);

    }
}
