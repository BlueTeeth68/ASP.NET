using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(object? id);

        Task<List<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        void Update (TEntity entity);

        Task DeleteByIdAsync(object? id);
        


    }
}
