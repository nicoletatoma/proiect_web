using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _10717proiect.Domain.Model.Event;

namespace _10717proiect.BusinessLogic.Core.DbDataContext
{
    public class EventContext : DbContext
    {
        public EventContext() : base("name=10717proiect")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EventContext>());
        }

        public virtual DbSet<EventDbTable> Events { get; set; }
    }
}