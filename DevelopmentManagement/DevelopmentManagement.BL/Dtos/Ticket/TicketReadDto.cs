using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.BL.Dtos
{
    public class TicketReadDto
    {
        public required  int Id { get; set; }
        public required string Description { get; set; } = string.Empty;
        public required int DevelpersCount { get; set; } 

    }
}
