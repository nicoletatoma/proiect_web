using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _10717proiect.Models.Event;
using _10717proiect.BusinessLogic;
using _10717proiect.Domain.Model;
using _10717proiect.BusinessLogic.BLStruct;
using _10717proiect.BusinessLogic.Interfaces;


namespace _10717proiect.Controllers
{
    public class CreateController : Controller
    {
        private readonly IEvent _eventCreate;

        public CreateController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _eventCreate = bl.CreateEventBL();
        }

        // GET: Events  
        public ActionResult Index()
        {
            return View();
        }

        // GET: Events/Create  
        public ActionResult Create()
        {
            return View(new EventDataModelView { EventDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(EventDataModelView createEvent)
        {
            string imagePath = null;

           
            if (createEvent.ImageFile != null && createEvent.ImageFile.ContentLength > 0)
            {
               
                string uploadsFolder = Server.MapPath("~/Content/Images/Events");

                
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
                imagePath = "/Content/Images/Events/" + uniqueFileName;
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
            return RedirectToAction("Index","Evenimente");
        }
    }
}