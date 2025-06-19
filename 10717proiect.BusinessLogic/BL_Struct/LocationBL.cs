using _10717proiect.BusinessLogic.Core;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.BL_Struct
{
     public class LocationBL : LocationAPI, ILocation
     {
          public string AddLocation(LocationData data)
          {
               return AddLocationAction(data);
          }

          public bool UpdateLocation(LocationData data)
          {
               return UpdateLocationAction(data);
          }

          public bool DeleteLocation(int locationId)
          {
               return DeleteLocationAction(locationId);
          }

          public LocationData GetLocationById(int locationId)
          {
               return GetLocationByIdAction(locationId);
          }

          public List<LocationData> GetAllLocations()
          {
               return GetAllLocationsAction();
          }
     }
}