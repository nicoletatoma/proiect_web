using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Models.Event;
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
          public ActionResult CreateEvent(EventDataModelView createEvent)
          {
               string imagePath = null;


               if (createEvent.ImageFile != null && createEvent.ImageFile.ContentLength > 0)
               {

                    string uploadsFolder = Server.MapPath("~/Content/Images/Location");


                    if (!System.IO.Directory.Exists(uploadsFolder))
                    {
                         System.IO.Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generați un nume unic pentru fișier pentru a evita suprascrierile
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" +
                                           System.IO.Path.GetFileName(createEvent.ImageFile.FileName);
                    string filePath = System.IO.Path.Combine(uploadsFolder, uniqueFileName);

                    // Salvați fișierul pe server
                    createEvent.ImageFile.SaveAs(filePath);

                    // Salvați calea relativă în baza de date
                    imagePath = "/Content/Images/Location/" + uniqueFileName;
               }

               var data = new _10717proiect.Domain.Model.Event.LocationData
               {
                    Name = createEvent.Name,
                    Description = createEvent.Description,
                    EventDate = createEvent.EventDate,
                    Location = createEvent.Location,
                    CategoryId = createEvent.CategoryId,
                    Price = createEvent.Price,
                    ImagePath = imagePath, // Folosiți calea nou generată
                    CreatedAt = DateTime.Now
               };

               string result = _eventCreate.CreateEventLogic(data);

               // Redirecționați către o pagină care arată toate evenimentele
               return RedirectToAction("Index", "Evenimente");
          }
     }
}
}