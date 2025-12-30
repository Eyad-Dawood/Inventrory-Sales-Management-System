using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(InventoryDbContext context) : base(context)
        {
        }

        public User GetByUserName(string UserName)
        {
            return _context.Users
                .Where(u=>u.Username == UserName)
                .FirstOrDefault();
        }
    }
}
