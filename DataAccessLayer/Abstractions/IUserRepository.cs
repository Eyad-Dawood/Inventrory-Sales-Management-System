using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
       public User GetByUserName(string userName);
    }
}
