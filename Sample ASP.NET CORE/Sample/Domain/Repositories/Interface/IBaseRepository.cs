using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interface
{
    public interface IBaseRepository<T> where T : class
    {

        IEnumerable<T> FindAll();

        T FindById(Object id);

        void Update(T entity);

        void DeleteById(Object id);

        void SaveChange();


    }
}
