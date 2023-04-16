using DevelopmentManagement.BL.Dtos;
using DevelopmentManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.BL
{
    public class TicketManager : ITicketManager
    {
        private readonly ITicketRepository ticketRepository;

        public TicketManager(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }
        public IEnumerable<TicketReadDto> GetAll()
        {
           var TicketsfromDb =   ticketRepository.GetAll();
            // to map to Dto

            return TicketsfromDb.Select(t => new TicketReadDto {
                Id = t.Id,
                Description = t.Title,
               DevelpersCount = t.developers.Count()
            });


        }
    }
}
