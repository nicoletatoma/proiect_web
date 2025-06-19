using _10717proiect.Domain.Model.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Interfaces
{
     public interface ILocation
     {
          string AddLocation(LocationData data);
          bool UpdateLocation(LocationData data);
          bool DeleteLocation(int locationId);
          LocationData GetLocationById(int locationId);
          List<LocationData> GetAllLocations();
     }
}