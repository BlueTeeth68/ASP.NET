using Infrastructure.Repositories.Interface;
using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
