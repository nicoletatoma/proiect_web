using _10717proiect.Domain.Model.Event;
using System;
using _10717proiect.Domain.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _10717proiect.Domain.Model.Comment;

namespace _10717proiect.BusinessLogic.DBContext
{
     public class CommentContext : DbContext
     {
          public CommentContext() : base("name=10717proiect")
          {
               Database.SetInitializer(new CreateDatabaseIfNotExists<EventContext>());
          }

          public virtual DbSet<CommentDbTable> Comments { get; set; }
     }
}
