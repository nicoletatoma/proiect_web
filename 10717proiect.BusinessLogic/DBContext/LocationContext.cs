using _10717proiect.Domain.Model.Event;
using _10717proiect.Domain.Model.Location;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.DBContext
{
     public class LocationContext : DbContext
     {
          public LocationContext() : base("name=10717proiect")
          {
               Database.SetInitializer(new CreateDatabaseIfNotExists<LocationContext>());
          }

          public virtual DbSet<LocationDbTable> Locations { get; set; }

     }
}
