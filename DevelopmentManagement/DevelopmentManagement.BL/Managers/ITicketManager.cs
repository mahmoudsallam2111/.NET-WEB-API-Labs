using DevelopmentManagement.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.BL
{
   public interface ITicketManager
    {
        IEnumerable<TicketReadDto> GetAll();
    }
}
