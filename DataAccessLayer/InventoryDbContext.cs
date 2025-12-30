using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class InventoryDbContext : DbContext
    {

        public DbSet<Person>People { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Customer>Customers { get; set; }
        public DbSet<Worker>Workers { get; set; }
        public DbSet<User>Users { get; set; }

        public InventoryDbContext(
         DbContextOptions<InventoryDbContext> options)
         : base(options)
        {
        }
    }
}
