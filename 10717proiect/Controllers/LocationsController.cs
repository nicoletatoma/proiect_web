using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.Location;
using _10717proiect.LogicHelper.Attributes;
using _10717proiect.Models.Location;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{
     // Removed [IsAdmin] attribute to allow public access
     public class LocationsController : BaseController
     {
          private readonly ILocation _location;

          public LocationsController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _location = bl.GetLocationBL();
          }

          // GET: Locations (Public view)
          public ActionResult Index()
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

          // Admin-only actions (keep these for admin functionality)
          [IsAdmin]
          public ActionResult Manage()
          {
               SessionStatus();
               if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
               {
                    return RedirectToAction("AuthIndex", "Auth");
               }

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

               return View("Manage", locationViewModels);
          }

          // POST: Location/AddLocation (Admin only)
          [HttpPost]
          [ValidateAntiForgeryToken]
          [IsAdmin]
          public ActionResult AddLocation(LocationViewModel model)
          {
               if (ModelState.IsValid)
               {
                    string imagePath = null;

                    // Procesare imagine dacă este furnizată
                    if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                    {
                         imagePath = SaveLocationImage(model.ImageFile, model.Name);
                         if (imagePath == null)
                         {
                              TempData["ErrorMessage"] = "Eroare la salvarea imaginii. Verificați formatul și mărimea fișierului.";
                              return RedirectToAction("Manage");
                         }
                    }

                    var locationData = new LocationData
                    {
                         Name = model.Name,
                         Description = model.Description,
                         Address = model.Address,
                         Phone = model.Phone,
                         ImagePath = imagePath
                    };

                    try
                    {
                         string result = _location.AddLocation(locationData);

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
                         TempData["ErrorMessage"] = "A apărut o eroare în sistem. Vă rugăm să încercați mai târziu.";
                         System.Diagnostics.Debug.WriteLine($"Error in AddLocation: {ex.Message}");
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

               return RedirectToAction("Manage");
          }

          // Other admin methods...
          [HttpPost]
          [ValidateAntiForgeryToken]
          [IsAdmin]
          public ActionResult UpdateLocation(LocationViewModel model)
          {
               // Your existing update logic
               return RedirectToAction("Manage");
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          [IsAdmin]
          public JsonResult DeleteLocation(int locationId)
          {
               // Your existing delete logic
               return Json(new { success = false, message = "Not implemented" });
          }

          // GET: Location/GetLocation (Public)
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

          private string SaveLocationImage(HttpPostedFileBase imageFile, string locationName)
          {
               // Your existing image saving logic
               return null;
          }

          private void DeleteLocationImage(string imagePath)
          {
               // Your existing image deletion logic
          }
     }
}