using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.Event;
using _10717proiect.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{

     public class EventsController : BaseController
     {
          private readonly IEventManager _eventManager;

          public EventsController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _eventManager = bl.GetEventManagerBL();
          }

          // GET: Events
          public ActionResult Index(string category = "", string location = "", string search = "", DateTime? date = null)
          {
               SessionStatus();

               try
               {
                    // Get all published events
                    List<EventData> events = _eventManager.GetPublishedEvents();

                    // Apply filters
                    if (!string.IsNullOrEmpty(category) && category != "toate")
                    {
                         events = events.Where(e => e.eventCategory.ToLower() == category.ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(location) && location != "toate")
                    {
                         events = events.Where(e => e.eventLocation.ToLower() == location.ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                         events = events.Where(e =>
                             e.eventName.ToLower().Contains(search.ToLower()) ||
                             (!string.IsNullOrEmpty(e.eventDescription) && e.eventDescription.ToLower().Contains(search.ToLower()))
                         ).ToList();
                    }

                    if (date.HasValue)
                    {
                         events = events.Where(e => e.eventDate.Date == date.Value.Date).ToList();
                    }

                    // Convert to view models
                    var eventViewModels = events.Select(e => new EventListViewModel
                    {
                         Id = e.Id,
                         eventName = e.eventName,
                         eventDescription = e.eventDescription,
                         eventDate = e.eventDate,
                         eventLocation = e.eventLocation,
                         eventCategory = e.eventCategory,
                         eventPrice = e.eventPrice,
                         eventImage = e.eventImage,
                         eventStatus = e.eventStatus,
                         createdAt = e.createdAt,
                         updatedAt = e.updatedAt
                    }).ToList();

                    // Store filter values for the view
                    ViewBag.SelectedCategory = category;
                    ViewBag.SelectedLocation = location;
                    ViewBag.SearchTerm = search;
                    ViewBag.SelectedDate = date;

                    return View(eventViewModels);
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in Events/Index: {ex.Message}");
                    return View(new List<EventListViewModel>());
               }
          }

          // GET: Events/Details/5
          public ActionResult Details(int id)
          {


               try
               {
                    // Get event details (we need to modify GetEventById to not require userId for public events)
                    var events = _eventManager.GetPublishedEvents();
                    var eventData = events.FirstOrDefault(e => e.Id == id);

                    if (eventData == null)
                    {
                         return HttpNotFound("Evenimentul nu a fost găsit sau nu este disponibil.");
                    }

                    var eventViewModel = new EventListViewModel
                    {
                         Id = eventData.Id,
                         eventName = eventData.eventName,
                         eventDescription = eventData.eventDescription,
                         eventDate = eventData.eventDate,
                         eventLocation = eventData.eventLocation,
                         eventCategory = eventData.eventCategory,
                         eventPrice = eventData.eventPrice,
                         eventImage = eventData.eventImage,
                         eventStatus = eventData.eventStatus,
                         createdAt = eventData.createdAt,
                         updatedAt = eventData.updatedAt
                    };

                    return View(eventViewModel);
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in Events/Details: {ex.Message}");
                    return HttpNotFound("A apărut o eroare la încărcarea evenimentului.");
               }
          }

          // GET: Events/Category/concert
          public ActionResult Category(string id)
          {
               return RedirectToAction("Index", new { category = id });
          }

          // GET: Events/Location/chisinau
          public ActionResult Location(string id)
          {
               return RedirectToAction("Index", new { location = id });
          }

          // AJAX: Get events by filters
          [HttpPost]
          public JsonResult FilterEvents(string category, string location, string search, DateTime? date)
          {
               try
               {
                    List<EventData> events = _eventManager.GetPublishedEvents();

                    // Apply filters
                    if (!string.IsNullOrEmpty(category) && category != "toate")
                    {
                         events = events.Where(e => e.eventCategory.ToLower() == category.ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(location) && location != "toate")
                    {
                         events = events.Where(e => e.eventLocation.ToLower() == location.ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                         events = events.Where(e =>
                             e.eventName.ToLower().Contains(search.ToLower()) ||
                             (!string.IsNullOrEmpty(e.eventDescription) && e.eventDescription.ToLower().Contains(search.ToLower()))
                         ).ToList();
                    }

                    if (date.HasValue)
                    {
                         events = events.Where(e => e.eventDate.Date == date.Value.Date).ToList();
                    }

                    var result = events.Select(e => new
                    {
                         id = e.Id,
                         eventName = e.eventName,
                         eventDescription = !string.IsNullOrEmpty(e.eventDescription) ?
                            (e.eventDescription.Length > 150 ? e.eventDescription.Substring(0, 150) + "..." : e.eventDescription) :
                            "Fără descriere",
                         eventDate = e.eventDate.ToString("dd MMMM yyyy, HH:mm"),
                         eventLocation = e.eventLocation,
                         eventCategory = e.eventCategory,
                         eventPrice = e.eventPrice,
                         eventImage = !string.IsNullOrEmpty(e.eventImage) ? Url.Content(e.eventImage) : Url.Content("~/Resources/Home/event.png")
                    }).ToList();

                    return Json(new { success = true, events = result });
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in FilterEvents: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la filtrarea evenimentelor." });
               }
          }
     }
}