using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.DAL
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
        IEnumerable<Ticket> GetAllAsNoTracked();
        Ticket? GetByID(int id);
        void Add(Ticket entity);

        void Update(Ticket entity);
        void Delete(Ticket entity);

        void SaveChanges(Ticket entity);


    }
}
