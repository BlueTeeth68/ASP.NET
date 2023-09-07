using Application;
using Application.Interfaces.Repositories;
using Domain;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository; }
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}