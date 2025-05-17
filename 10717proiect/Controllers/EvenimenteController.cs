using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Model.Event;

namespace _10717proiect.Controllers
{
    public class EvenimenteController : Controller
    {
        private readonly EventContext _context;

        public EvenimenteController()
        {
            _context = new EventContext();
        }

        public ActionResult Index()
        {
            var evenimente = _context.Events.ToList(); 
            return View(evenimente); 
        }
    }
}