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
            var data = new _10717proiect.Domain.Model.Event.EventDataModel
            {
                Name = createEvent.Name,
                Description = createEvent.Description,
                EventDate = createEvent.EventDate,
                Location = createEvent.Location,
                CategoryId = createEvent.CategoryId,
                Price = createEvent.Price,
                ImagePath = createEvent.ImagePath,
                CreatedAt = DateTime.Now
            };

            string result = _eventCreate.CreateEventLogic(data);

            return View("Index");
        }
    }
}