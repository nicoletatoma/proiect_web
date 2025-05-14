using _10717proiect.Domain.User;
using System.Data.Entity;

namespace _10717proiect.BusinessLogic.Core.DbDataContext
{
    public class UserContext : DbContext
    {
       public UserContext() : base("name=10717proiect") { }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
