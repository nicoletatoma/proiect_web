using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Models.Event;
using _10717proiect.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocation _location;

        public LocationsController()
        {
             var bl = new BusinessLogic.BusinessLogic();
             _location = bl.GetLocationBL();
        }
        // GET: Locations
        public ActionResult Index()
        {
            return View();
        }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CreateLocation(LocationDataView createLocation)
          {
               string imagePath = null;


               if (createLocation.ImageFile != null && createLocation.ImageFile.ContentLength > 0)
               {

                    string uploadsFolder = Server.MapPath("~/Content/Images/Location");


                    if (!System.IO.Directory.Exists(uploadsFolder))
                    {
                         System.IO.Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generați un nume unic pentru fișier pentru a evita suprascrierile
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" +
                                           System.IO.Path.GetFileName(createLocation.ImageFile.FileName);
                    string filePath = System.IO.Path.Combine(uploadsFolder, uniqueFileName);

                    // Salvați fișierul pe server
                    createLocation.ImageFile.SaveAs(filePath);

                    // Salvați calea relativă în baza de date
                    imagePath = "/Content/Images/Location/" + uniqueFileName;
               }

               var data = new _10717proiect.Domain.Model.Location.LocationData
               {
                    Name = createLocation.Name,
                    Description = createLocation.Description,
                    Address = createLocation.Address,
                    Phone = createLocation.Phone,
                    ImagePath = imagePath // Folosiți calea nou generată
               };

               string result = _location.AddLocation(data);

               // Redirecționați către o pagină care arată toate locatiile
               return RedirectToAction("Index", "Locations");
          }
     }
}