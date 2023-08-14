using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<int> AddAsync(TEntity entity); 

        Task<int> Update(TEntity entity);

        Task DeleteById(int id);

    }
}
