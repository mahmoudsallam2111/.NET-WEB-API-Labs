using DevelopmentManagement.BL;
using DevelopmentManagement.BL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevelopmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketManager ticketManager;

        public TicketController(ITicketManager ticketManager)
        {
            this.ticketManager = ticketManager;
        }
        [HttpGet]
        public ActionResult<IEnumerable<TicketReadDto>> GetAll()
        {
            return ticketManager.GetAll().ToList(); 
        }
    }
}
