using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentManagement.DAL
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DevelopementContext context;

        public TicketRepository(DevelopementContext Context)
        {
            context = Context;
        }

        public IEnumerable<Ticket> GetAll()
        {
            return context.Set<Ticket>().ToList();
            // return context.Tickets.ToList();
        }


        public IEnumerable<Ticket> GetAllAsNoTracked()
        {
            return context.Set<Ticket>().AsNoTracking();
            // return context.Tickets.ToList();
        }

        public Ticket? GetByID(int id)
        {
            return context.Set<Ticket>().Find(id); // if the entity is tracked it will not go to the database   
        }
        public void Add(Ticket entity)
        {
            context.Add(entity);
        }

        /*
         if i want to not track entity when retrived from db i call AsNoTracking
         return context.Set<Ticket>().AsNoTracking().Find(id); 
        note:
        1-tracking is a cost opration
         */
        public void Update(Ticket entity)  // only necessary if i do not use tracking .
        {
         
        }

        public void Delete(Ticket entity)
        {
            context.Set<Ticket>().Remove(entity);
        }

// ==> to minimize trips to database  , as EF is working by tracking . this will improve performance of Application
        public void SaveChanges(Ticket entity)  
        {
            context.SaveChanges();
        }
    }
}
