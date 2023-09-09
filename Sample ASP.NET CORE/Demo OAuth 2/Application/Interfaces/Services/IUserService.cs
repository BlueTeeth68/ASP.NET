using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ReturnLoginUserDTO?> CreateNewAsync(CreateUserDTO createUserDto);

        Task<List<UserDTO>> GetAllAsync();

        Task<UserDTO> GetByIdAsync(int id);

        Task<int> DeleteAsync(int id);

        public Task<ReturnLoginUserDTO?> LoginAsync(UserLogin userLogin);

        //Task<UserDTO> UpdateAsync(UserDTO userDTO);

    }
}
