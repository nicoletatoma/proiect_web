using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Model.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Core
{
     public class LocationAPI
     {
          internal string AddLocationAction(LocationData data)
          {
               if (data == null ||
                   string.IsNullOrEmpty(data.Name) ||
                   string.IsNullOrEmpty(data.Description) ||
                   string.IsNullOrEmpty(data.Address) ||
                   string.IsNullOrEmpty(data.Phone)
                   )
               {
                    return null;
               }

               using (var dbContext = new LocationContext())
               {
                    var newEvent = new LocationDbTable
                    {
                         Name = data.Name,
                         Description = data.Description,
                         Address = data.Address,
                         Phone = data.Address,
                         ImagePath = data.ImagePath
                    };
                    dbContext.Locations.Add(newEvent);
                    dbContext.SaveChanges();
                    return "Location added successfully!";
               }



          }
     }
}
