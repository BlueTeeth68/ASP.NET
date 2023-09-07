using Application.DTOs;
using Application.Interfaces.Repositories;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository 
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Username.Trim().ToLower().Equals(username.Trim().ToLower()));
        }


        public async Task<User> LoginAsync(UserLogin userLogin)
        {
            var user = await GetByUsernameAsync(userLogin.Username);
            if(user != null)
            {
                //compare password
                if (user.PasswordHash.Equals(userLogin.Password))
                    return user;
            }
            return null;
        }

    }
}
