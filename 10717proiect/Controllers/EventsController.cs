using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.Event;

namespace _10717proiect.Controllers
{

     public class EventsController : Controller
     {
          //private readonly IEvent _product;

          //public EvenimenteController()
          //{
          //     var bl = new BusinessLogic.BusinessLogic();
          //     _product = bl.CreateEventBL();
          //}

          public ActionResult Index()
          {
               using (var context = new EventContext())
               {
                    var evenimente = context.Events.ToList();
                    return View(evenimente);
               }
          }
     }
}