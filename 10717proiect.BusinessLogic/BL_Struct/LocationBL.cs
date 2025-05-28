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

     }
}
