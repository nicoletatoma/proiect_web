using _10717proiect.Domain.Model.Events;
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
        public DbSet<EDbTable> Evenimente { get; set; }

        public EventContext() : base("DefaultConnection")
        {
        }
    }
}
