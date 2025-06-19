using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Enums;
using _10717proiect.Domain.Model.Admin;
using _10717proiect.Domain.Model.Event;
using _10717proiect.Domain.Model.Location;
using _10717proiect.LogicHelper.Attributes;
using _10717proiect.Models.Event;
using _10717proiect.Models.Location;
using _10717proiect.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
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
          private readonly ILocation _location;

          public AdminController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _eventManager = bl.GetEventManagerBL();
               _location = bl.GetLocationBL();
          }

          public ActionResult Index()
          {
               return View();
          }

          #region EVENIMENTE

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

               var eventViewModels = events.Select(e => new EventViewModel
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

               // Adaugă locațiile în ViewBag pentru dropdown
               var locations = _location.GetAllLocations();
               ViewBag.Locations = locations.Select(l => new SelectListItem
               {
                    Value = l.Name.ToLower().Replace(" ", "-"),
                    Text = l.Name
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
                                   eventImage = !string.IsNullOrEmpty(eventData.eventImage) ? Url.Content(eventData.eventImage) : null,
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

          #endregion

          #region LOCAȚII

          // GET: Admin/Locations
          public ActionResult Locations()
          {
               SessionStatus();
               if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
               {
                    return RedirectToAction("AuthIndex", "Auth");
               }

               try
               {
                    var locations = _location.GetAllLocations();
                    var locationViewModels = locations.Select(l => new LocationViewModel
                    {
                         Id = l.Id,
                         Name = l.Name,
                         Description = l.Description,
                         Address = l.Address,
                         Phone = l.Phone,
                         ImagePath = l.ImagePath
                    }).ToList();

                    return View(locationViewModels);
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in Admin/Locations: {ex.Message}");
                    return View(new List<LocationViewModel>());
               }
          }

          // POST: Admin/AddLocation
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult AddLocation(LocationViewModel model)
          {
               System.Diagnostics.Debug.WriteLine("AddLocation called");
               System.Diagnostics.Debug.WriteLine($"Model state valid: {ModelState.IsValid}");
               System.Diagnostics.Debug.WriteLine($"Name: {model.Name}");
               System.Diagnostics.Debug.WriteLine($"Address: {model.Address}");

               if (ModelState.IsValid)
               {
                    string imagePath = null;

                    // Procesare imagine dacă este furnizată
                    if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                    {
                         System.Diagnostics.Debug.WriteLine("Processing image file");
                         imagePath = SaveLocationImage(model.ImageFile, model.Name);
                         if (imagePath == null)
                         {
                              System.Diagnostics.Debug.WriteLine("Image save failed");
                              TempData["ErrorMessage"] = "Eroare la salvarea imaginii. Verificați formatul și mărimea fișierului.";
                              return RedirectToAction("Locations");
                         }
                         System.Diagnostics.Debug.WriteLine($"Image saved: {imagePath}");
                    }

                    var locationData = new LocationData
                    {
                         Name = model.Name,
                         Description = "", // Set empty string instead of required
                         Address = model.Address,
                         Phone = "", // Set empty string instead of required
                         ImagePath = imagePath
                    };

                    try
                    {
                         System.Diagnostics.Debug.WriteLine("Calling _location.AddLocation");
                         string result = _location.AddLocation(locationData);
                         System.Diagnostics.Debug.WriteLine($"AddLocation result: {result}");

                         if (!string.IsNullOrEmpty(result))
                         {
                              TempData["SuccessMessage"] = "Locația a fost adăugată cu succes!";
                         }
                         else
                         {
                              TempData["ErrorMessage"] = "A apărut o eroare la adăugarea locației. Verificați datele introduse.";
                         }
                    }
                    catch (Exception ex)
                    {
                         System.Diagnostics.Debug.WriteLine($"Exception in AddLocation: {ex.Message}");
                         System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                         TempData["ErrorMessage"] = "A apărut o eroare în sistem. Vă rugăm să încercați mai târziu.";
                    }
               }
               else
               {
                    System.Diagnostics.Debug.WriteLine("Model state invalid");
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                         System.Diagnostics.Debug.WriteLine($"Model error: {error.ErrorMessage}");
                    }

                    var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);
                    string errorMessage = string.Join(". ", errorMessages);
                    TempData["ErrorMessage"] = "Datele introduse nu sunt valide. " + errorMessage;
               }

               return RedirectToAction("Locations");
          }

          // POST: Admin/UpdateLocation
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult UpdateLocation(LocationViewModel model)
          {
               if (ModelState.IsValid)
               {
                    string imagePath = model.ImagePath; // Păstrează imaginea existentă

                    // Procesare imagine nouă dacă este furnizată
                    if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                    {
                         var newImagePath = SaveLocationImage(model.ImageFile, model.Name);
                         if (newImagePath != null)
                         {
                              // Șterge imaginea veche dacă există
                              if (!string.IsNullOrEmpty(model.ImagePath))
                              {
                                   DeleteLocationImage(model.ImagePath);
                              }
                              imagePath = newImagePath;
                         }
                    }

                    var locationData = new LocationData
                    {
                         Id = model.Id,
                         Name = model.Name,
                         Description = "", // Set empty string instead of required
                         Address = model.Address,
                         Phone = "", // Set empty string instead of required
                         ImagePath = imagePath
                    };

                    try
                    {
                         bool isUpdated = _location.UpdateLocation(locationData);

                         if (isUpdated)
                         {
                              TempData["SuccessMessage"] = "Locația a fost actualizată cu succes!";
                         }
                         else
                         {
                              TempData["ErrorMessage"] = "Nu s-a putut actualiza locația.";
                         }
                    }
                    catch (Exception ex)
                    {
                         TempData["ErrorMessage"] = "A apărut o eroare în sistem. Vă rugăm să încercați mai târziu.";
                         System.Diagnostics.Debug.WriteLine($"Error in UpdateLocation: {ex.Message}");
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

               return RedirectToAction("Locations");
          }

          // POST: Admin/DeleteLocation
          [HttpPost]
          [ValidateAntiForgeryToken]
          public JsonResult DeleteLocation(int locationId)
          {
               try
               {
                    bool isDeleted = _location.DeleteLocation(locationId);

                    if (isDeleted)
                    {
                         return Json(new { success = true, message = "Locația a fost ștearsă cu succes!" });
                    }
                    else
                    {
                         return Json(new { success = false, message = "Nu s-a putut șterge locația." });
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in DeleteLocation: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la ștergerea locației." });
               }
          }

          // GET: Admin/GetLocation
          [HttpGet]
          public JsonResult GetLocation(int locationId)
          {
               try
               {
                    var locationData = _location.GetLocationById(locationId);

                    if (locationData != null)
                    {
                         var result = new
                         {
                              success = true,
                              data = new
                              {
                                   id = locationData.Id,
                                   name = locationData.Name,
                                   description = locationData.Description,
                                   address = locationData.Address,
                                   phone = locationData.Phone,
                                   imagePath = !string.IsNullOrEmpty(locationData.ImagePath) ? Url.Content(locationData.ImagePath) : null
                              }
                         };

                         return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                         return Json(new { success = false, message = "Locația nu a fost găsită." }, JsonRequestBehavior.AllowGet);
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetLocation: {ex.Message}");
                    return Json(new { success = false, message = "A apărut o eroare la încărcarea locației." }, JsonRequestBehavior.AllowGet);
               }
          }

          #endregion

          #region HELPER METHODS

          private string SaveLocationImage(HttpPostedFileBase imageFile, string locationName)
          {
               try
               {
                    // Validare tip fișier
                    var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
                    if (!allowedTypes.Contains(imageFile.ContentType.ToLower()))
                    {
                         return null;
                    }

                    // Validare dimensiune (5MB)
                    if (imageFile.ContentLength > 5 * 1024 * 1024)
                    {
                         return null;
                    }

                    // Creare structură directoare
                    string baseLocationsPath = HttpContext.Server.MapPath("~/Content/Images/Locations");
                    string locationPath = Path.Combine(baseLocationsPath, locationName.Replace(" ", "_"));

                    if (!Directory.Exists(baseLocationsPath))
                    {
                         Directory.CreateDirectory(baseLocationsPath);
                    }

                    if (!Directory.Exists(locationPath))
                    {
                         Directory.CreateDirectory(locationPath);
                    }

                    // Generare nume unic pentru fișier
                    string fileExtension = Path.GetExtension(imageFile.FileName);
                    string fileName = $"{Guid.NewGuid()}{fileExtension}";
                    string filePath = Path.Combine(locationPath, fileName);

                    // Salvare fișier
                    imageFile.SaveAs(filePath);

                    // Returnare path relativ
                    return $"~/Content/Images/Locations/{locationName.Replace(" ", "_")}/{fileName}";
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error saving location image: {ex.Message}");
                    return null;
               }
          }

          private void DeleteLocationImage(string imagePath)
          {
               try
               {
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                         string physicalPath = HttpContext.Server.MapPath(imagePath);
                         if (System.IO.File.Exists(physicalPath))
                         {
                              System.IO.File.Delete(physicalPath);
                         }
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error deleting location image: {ex.Message}");
               }
          }

          [HttpGet]
          public JsonResult GetAvailableLocations()
          {
               try
               {
                    var locations = _location.GetAllLocations();
                    var locationList = locations.Select(l => new
                    {
                         value = l.Name.ToLower().Replace(" ", "-"),
                         text = l.Name
                    }).ToList();

                    return Json(new { success = true, locations = locationList }, JsonRequestBehavior.AllowGet);
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetAvailableLocations: {ex.Message}");
                    return Json(new { success = false, message = "Eroare la încărcarea locațiilor." }, JsonRequestBehavior.AllowGet);
               }
          }
          #endregion

          #region EXISTING METHODS

          public ActionResult Transactions()
          {
               return View();
          }

          #endregion
     }
}