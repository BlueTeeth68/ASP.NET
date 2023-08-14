using Domain.Data;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implements
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
           await _dbSet.AddAsync(entity);
           return await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            TEntity entity = await GetByIdAsync(id);
            if(entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
           return await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<int> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
           return await _context.SaveChangesAsync();
        }
    }
}
