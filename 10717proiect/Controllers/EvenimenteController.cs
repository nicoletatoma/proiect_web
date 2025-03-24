using _10717proiect.Domain.Model.Events;
using _10717proiect.Models.Events;
using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{
    public class EvenimenteController : Controller
    {
        // GET: Evenimente

        private readonly _10717proiect.BusinessLogic.Interfaces.IEvent _event;
        public EvenimenteController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _event = bl.GetEventBL();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEvent(Event data)
        {
            
            var details = new EventDTO
            {
                Nume = data.Nume,
                Categorie = data.Categorie,
                Pret = data.Pret,
                Data = data.Data,
                Locatie = data.Locatie,
                Descriere = data.Descriere,
                ImagineURL = data.ImagineURL
            };
            string token = _event.UserEventLogic(details);  


            return View();
        }

        
    }
}