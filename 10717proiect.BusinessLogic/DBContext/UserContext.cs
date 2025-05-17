using _10717proiect.Domain.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.DBContext
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=10717proiect") { }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
