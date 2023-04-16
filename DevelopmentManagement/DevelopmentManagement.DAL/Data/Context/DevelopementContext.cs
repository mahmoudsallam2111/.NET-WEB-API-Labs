using DevelopmentManagement.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.DAL
{
   public  class DevelopementContext:DbContext
    {
        public DbSet<Ticket> Tickets => Set<Ticket>();    // as tickets never can be set propery
        public DbSet<Developer> Developers => Set<Developer>();
        public DbSet<Department> Departments => Set<Department>();

        public DevelopementContext(DbContextOptions<DevelopementContext> options):base(options)
        {
            
        }


    }
}
