using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(object id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Add(TEntity entity);

        Task Update (TEntity entity);   


        


    }
}
