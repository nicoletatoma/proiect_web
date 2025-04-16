using _10717proiect.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _10717proiect.BusinessLogic.DbContext
{
     public class SearchContext : DbContextTransaction
     {
          public SearchContext() : base("name=TiketExpresShop")
          {
          }

          public DbSet<SearchEv> Evenimente { get; set; }
     }
}
