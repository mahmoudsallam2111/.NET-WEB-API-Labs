using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.DAL
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DevelopementContext context;

        public DepartmentRepository(DevelopementContext Context)
        {
            context = Context;
        }
        public Department? getdepartmentdetailswithTickets(int id)
        {
            return context.Set<Department>().
                Include(t=>t.tickets).
                           ThenInclude(d=>d.developers).
                FirstOrDefault(d=>d.Id==id);
           
        }

        public void SaveChanges(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
