using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.Event;
using _10717proiect.Models.Tickets;

namespace _10717proiect.Controllers
{
    public class TicketController : BaseController
    {
        private readonly IEvent _eventService;

        public TicketController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _eventService = bl.CreateEventBL();
        }

        // GET: Tickets
        public ActionResult Index()
        {
            // Verificăm sesiunea utilizatorului
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // Simulez câteva bilete pentru demonstrație
            // În realitate, acestea ar trebui să vină din baza de date
            var tickets = GetUserTickets();

            return View(tickets);
        }

        // Metodă pentru a obține biletele utilizatorului
        private List<TicketViewModel> GetUserTickets()
        {
            // Simulez date pentru demonstrație
            // În implementarea reală, acestea ar trebui să vină din baza de date
            return new List<TicketViewModel>
            {
                new TicketViewModel
                {
                    TicketId = "B4142623656",
                    EventName = "Dan Balan - Christmas Magic",
                    EventImage = "/Content/assets/images/dan-balan.jpg",
                    PurchaseDate = new DateTime(2023, 12, 21, 14, 31, 0),
                    EventDate = new DateTime(2023, 12, 24, 19, 0, 0),
                    Location = "Arena Chișinău",
                    Price = 500,
                    Quantity = 1,
                    Status = "Valabil"
                },
                new TicketViewModel
                {
                    TicketId = "B4142623657",
                    EventName = "Concert Carla's Dreams",
                    EventImage = "/Content/assets/images/carlas-dreams.jpg",
                    PurchaseDate = new DateTime(2023, 11, 15, 10, 20, 0),
                    EventDate = new DateTime(2024, 1, 20, 20, 0, 0),
                    Location = "Palatul Național",
                    Price = 350,
                    Quantity = 2,
                    Status = "Valabil"
                },
                new TicketViewModel
                {
                    TicketId = "B4142623658",
                    EventName = "Teatru - Hamlet",
                    EventImage = "/Content/assets/images/hamlet.jpg",
                    PurchaseDate = new DateTime(2023, 10, 5, 16, 45, 0),
                    EventDate = new DateTime(2023, 11, 10, 19, 30, 0),
                    Location = "Teatrul Național",
                    Price = 120,
                    Quantity = 1,
                    Status = "Expirat"
                }
            };
        }

        // Acțiune pentru a vizualiza detaliile unui bilet
        public ActionResult Details(string ticketId)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("SignIn", "Auth");
            }

            var tickets = GetUserTickets();
            var ticket = tickets.FirstOrDefault(t => t.TicketId == ticketId);

            if (ticket == null)
            {
                return HttpNotFound();
            }

            return View(ticket);
        }

        // Acțiune pentru a descărca biletul
        public ActionResult DownloadTicket(string ticketId)
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("SignIn", "Auth");
            }

            var tickets = GetUserTickets();
            var ticket = tickets.FirstOrDefault(t => t.TicketId == ticketId);

            if (ticket == null)
            {
                return HttpNotFound();
            }

            // Aici ar trebui să generezi PDF-ul biletului
            // Pentru demonstrație, redirectez înapoi
            TempData["Message"] = "Biletul a fost descărcat cu succes!";
            return RedirectToAction("Index");
        }
    }
}