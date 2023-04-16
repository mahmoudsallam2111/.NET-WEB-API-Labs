using DevelopmentManagement.BL.Dtos;
using DevelopmentManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.BL
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        public DepartmentReadDto? getById(int id)
        {
          Department?  departmentFromDB = departmentRepository.getdepartmentdetailswithTickets(id);

            if (departmentFromDB is null)
            {
                return null;
            }

            return new DepartmentReadDto
            {
                Id = departmentFromDB.Id,
                Name = departmentFromDB.Name,
                tickets = departmentFromDB.tickets.Select(t => new TicketReadDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    DevelpersCount = t.developers.Count
                }).ToList()
            };

        }
    }
}
