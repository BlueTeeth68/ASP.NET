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

        public BaseRepository(ApplicationDbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public void DeleteById(int id)
        {
            TEntity entity = GetByIdAsync(id).Result;
            if(entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            return result;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var result = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
