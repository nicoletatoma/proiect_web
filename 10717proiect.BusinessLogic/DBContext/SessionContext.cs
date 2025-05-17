using _10717proiect.Domain.Model.Session;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.DBContext
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=10717proiect") { }

        public virtual DbSet<USessionDbTable> Session { get; set; }

    }
}
