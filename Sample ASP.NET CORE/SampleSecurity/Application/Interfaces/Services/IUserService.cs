using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateNewAsync(CreateUserDTO createUserDTO);

        Task<List<UserDTO>> GetAllAsync();

        Task<UserDTO> GetByIdAsync(int id);

        Task<int> DeleteAsync(int id);

        public Task<ReturnLoginUserDTO> LoginAsync(UserLogin userLogin);

        //Task<UserDTO> UpdateAsync(UserDTO userDTO);

    }
}
