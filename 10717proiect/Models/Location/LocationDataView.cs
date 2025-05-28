using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _10717proiect.Models.Location
{
     public class LocationDataView
     {
          public string Name { get; set; }
          public string Description { get; set; }
          public string Address { get; set; }
          public string Phone { get; set; }
          public string ImagePath { get; set; }
          public HttpPostedFileBase ImageFile { get; set; }
     }
}