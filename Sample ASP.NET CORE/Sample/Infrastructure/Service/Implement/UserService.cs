using Domain.Repositories.Interface;
using Infrastructure.Service.Interface;
using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service.Implement
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<int> CreateUser(User user)
        {
            return await userRepository.AddAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            await userRepository.DeleteById(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public async Task<int> UpdateUser(User user)
        {
            return await userRepository.Update(user);
        }
    }
}
