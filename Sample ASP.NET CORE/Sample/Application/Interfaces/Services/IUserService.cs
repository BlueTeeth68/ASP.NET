using Application.DTOs.User;
using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {

        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<UserDTO> CreateUser(CreateUserDTO createUserDTO);
        Task DeleteUser(int id);
        Task<UserDTO> UpdateUser(User user);

    }
}
