using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.DAL
{
    public interface IDepartmentRepository
    {
        Department? getdepartmentdetailswithTickets(int id);

        void SaveChanges(Department entity);
    }
}
