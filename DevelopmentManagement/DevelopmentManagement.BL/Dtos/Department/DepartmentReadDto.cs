using DevelopmentManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.BL.Dtos
{
    public class DepartmentReadDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public  required List<TicketReadDto> tickets { get; set; } = new();
    }
}
