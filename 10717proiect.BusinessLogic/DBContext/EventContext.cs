using _10717proiect.Domain.Model.Event;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.DBContext
{

     public class EventContext : DbContext
     {
          public EventContext() : base("name=10717proiect")
          {

          }

          public virtual DbSet<EventDbTable> Events { get; set; }
     }
}
