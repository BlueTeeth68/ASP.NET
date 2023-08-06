using Domain.Data;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implements
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public BaseRepository()
        {
            dbContext = new ApplicationDbContext();

        }

        public void DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public T FindById(object id)
        {
            throw new NotImplementedException();
        }

        public void SaveChange()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
