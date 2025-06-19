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
     [IsAdmin]
     public class LocationController : BaseController
     {
          private readonly ILocation _location;

          public LocationController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _location = bl.GetLocationBL();
          }

          // GET: Location
          public ActionResult Index()
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

               return View(locationViewModels);
          }

          // POST: Location/AddLocation
          [HttpPost]
          [ValidateAntiForgeryToken]
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
                              return RedirectToAction("Index");
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

               return RedirectToAction("Index");
          }

          // POST: Location/UpdateLocation
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
                         Description = model.Description,
                         Address = model.Address,
                         Phone = model.Phone,
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

               return RedirectToAction("Index");
          }

          // POST: Location/DeleteLocation
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

          // GET: Location/GetLocation
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
     }
}