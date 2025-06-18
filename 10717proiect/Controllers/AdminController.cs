using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Enums;
using _10717proiect.Domain.Model.Admin;
using _10717proiect.Domain.Model.Event;
using _10717proiect.LogicHelper.Attributes;
using _10717proiect.Models.Event;
using _10717proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{
     [IsAdmin]
     public class AdminController : BaseController
     {
          private readonly IEventManager _eventManager;

          public AdminController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _eventManager = bl.GetEventManagerBL();
          }
          public ActionResult Index()
          {
               return View();
          }
          public ActionResult Location()
          {
               return View();
          }

          public ActionResult Transactions()
          {
               return View();
          }
          // GET: Admin/Events
          public ActionResult Events()
          {
               SessionStatus();
               if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
               {
                    return RedirectToAction("AuthIndex", "Auth");
               }

               int userId = (int)System.Web.HttpContext.Current.Session["UserId"];
               var events = _eventManager.GetUserEvents(userId);

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

               return View(eventViewModels);
          }

          // POST: Admin/AddEvent
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> AddEvent(EventViewModel model)
          {
               if (ModelState.IsValid)
               {
                    int userId = (int)System.Web.HttpContext.Current.Session["UserId"];

                    var eventData = new EventData
                    {
                         eventName = model.eventName,
                         eventDescription = model.eventDescription,
                         eventDate = model.eventDate,
                         eventLocation = model.eventLocation,
                         eventCategory = model.eventCategory,
                         eventPrice = model.eventPrice,
                         eventStatus = model.eventStatus
                    };

                    try
                    {
                         bool isCreated = await _eventManager.CreateEvent(eventData, model.eventImageFile, userId);

                         if (isCreated)
                         {
                              TempData["SuccessMessage"] = "Evenimentul a fost adăugat cu succes!";
                              return RedirectToAction("Events");
                         }
                         else
                         {
                              TempData["ErrorMessage"] = "A apărut o eroare la adăugarea evenimentului. Verificați datele introduse.";
                         }
                    }
                    catch (Exception ex)
                    {
                         TempData["ErrorMessage"] = "A apărut o eroare în sistem. Vă rugăm să încercați mai târziu.";
                         System.Diagnostics.Debug.WriteLine($"Error in AddEvent: {ex.Message}");
                    }
               }
               else
               {
                    var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);
                    string errorMessage = string.Join(". ", errorMessages);
                    TempData["ErrorMessage"] = "Datele introduse nu sunt valide. " + errorMessage;
               }

               return RedirectToAction("Events");
          }

          // POST: Admin/UpdateEvent
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult UpdateEvent(EventViewModel model)
          {
               if (ModelState.IsValid)
               {
                    int userId = (int)System.Web.HttpContext.Current.Session["UserId"];

                    var eventData = new EventData
                    {
                         Id = model.Id,
                         eventName = model.eventName,
                         eventDescription = model.eventDescription,
                         eventDate = model.eventDate,
                         eventLocation = model.eventLocation,
                         eventCategory = model.eventCategory,
                         eventPrice = model.eventPrice,
                         eventStatus = model.eventStatus
                    };

                    try
                    {
                         bool isUpdated = _eventManager.UpdateEvent(eventData, model.eventImageFile, userId);

                         if (isUpdated)
                         {
                              TempData["SuccessMessage"] = "Evenimentul a fost actualizat cu succes!";
                         }
                         else
                         {
                              TempData["ErrorMessage"] = "Nu s-a putut actualiza evenimentul. Verificați dacă evenimentul vă aparține.";
                         }
                    }
                    catch (Exception ex)
                    {
                         TempData["ErrorMessage"] = "A apărut o eroare în sistem. Vă rugăm să încercați mai târziu.";
                         System.Diagnostics.Debug.WriteLine($"Error in UpdateEvent: {ex.Message}");
                    }
               }
               else
               {
                    var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);
                    string errorMessage = string.Join(". ", errorMessages);
                    TempData["ErrorMessage"] = "Datele introduse nu sunt valide. " + errorMessage;
               }

               return RedirectToAction("Events");
          }

          // POST: Admin/DeleteEvent
          [HttpPost]
          [ValidateAntiForgeryToken]
          public JsonResult DeleteEvent(int eventId)
          {
               try
               {
                    int userId = (int)System.Web.HttpContext.Current.Session["UserId"];
                    bool isDeleted = _eventManager.DeleteEvent(eventId, userId);

                    if (isDeleted)
                    {
                         return Json(new { success = true, message = "Evenimentul a fost șters cu succes!" });
                    }
                    else
                    {
                         return Json(new { success = false, message = "Nu s-a putut șterge evenimentul." });
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in DeleteEvent: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la ștergerea evenimentului." });
               }
          }

          // POST: Admin/UpdateEventStatus
          [HttpPost]
          [ValidateAntiForgeryToken]
          public JsonResult UpdateEventStatus(int eventId, int status)
          {
               try
               {
                    int userId = (int)System.Web.HttpContext.Current.Session["UserId"];
                    var eventStatus = (EventStatus)status;

                    bool isUpdated = _eventManager.UpdateEventStatus(eventId, eventStatus, userId);

                    if (isUpdated)
                    {
                         return Json(new { success = true, message = "Statusul evenimentului a fost actualizat!" });
                    }
                    else
                    {
                         return Json(new { success = false, message = "Nu s-a putut actualiza statusul evenimentului." });
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in UpdateEventStatus: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la actualizarea statusului." });
               }
          }

          // GET: Admin/GetEvent
          [HttpGet]
          public JsonResult GetEvent(int eventId)
          {
               try
               {
                    int userId = (int)System.Web.HttpContext.Current.Session["UserId"];
                    var eventData = _eventManager.GetEventById(eventId, userId);

                    if (eventData != null)
                    {
                         var result = new
                         {
                              success = true,
                              data = new
                              {
                                   id = eventData.Id,
                                   eventName = eventData.eventName,
                                   eventDescription = eventData.eventDescription,
                                   eventDate = eventData.eventDate.ToString("yyyy-MM-ddTHH:mm"),
                                   eventLocation = eventData.eventLocation,
                                   eventCategory = eventData.eventCategory,
                                   eventPrice = eventData.eventPrice,
                                   eventImage = eventData.eventImage,
                                   eventStatus = (int)eventData.eventStatus
                              }
                         };

                         return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                         return Json(new { success = false, message = "Evenimentul nu a fost găsit." }, JsonRequestBehavior.AllowGet);
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetEvent: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la încărcarea evenimentului." }, JsonRequestBehavior.AllowGet);
               }
          }
     }
}