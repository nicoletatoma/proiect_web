using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Model.Location;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _10717proiect.BusinessLogic.Core
{
     public class LocationAPI
     {
          internal string AddLocationAction(LocationData data)
          {
               try
               {
                    if (data == null ||
                        string.IsNullOrEmpty(data.Name) ||
                        string.IsNullOrEmpty(data.Address))
                    {
                         System.Diagnostics.Debug.WriteLine("AddLocationAction: Invalid data provided");
                         return null;
                    }

                    using (var dbContext = new LocationContext())
                    {
                         var newLocation = new LocationDbTable
                         {
                              Name = data.Name,
                              Description = data.Description ?? "",
                              Address = data.Address,
                              Phone = data.Phone ?? "",
                              ImagePath = data.ImagePath
                         };

                         System.Diagnostics.Debug.WriteLine($"Adding location: {data.Name} at {data.Address}");
                         dbContext.Locations.Add(newLocation);
                         dbContext.SaveChanges();
                         System.Diagnostics.Debug.WriteLine("Location added successfully");
                         return "Location added successfully!";
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in AddLocationAction: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                    return null;
               }
          }

          internal bool UpdateLocationAction(LocationData data)
          {
               try
               {
                    using (var db = new LocationContext())
                    {
                         var existingLocation = db.Locations.FirstOrDefault(l => l.Id == data.Id);

                         if (existingLocation == null)
                         {
                              return false;
                         }

                         // Actualizare date
                         existingLocation.Name = data.Name;
                         existingLocation.Description = data.Description;
                         existingLocation.Address = data.Address;
                         existingLocation.Phone = data.Phone;

                         if (!string.IsNullOrEmpty(data.ImagePath))
                         {
                              existingLocation.ImagePath = data.ImagePath;
                         }

                         db.Entry(existingLocation).State = EntityState.Modified;
                         db.SaveChanges();

                         return true;
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in UpdateLocationAction: {ex.Message}");
                    return false;
               }
          }

          internal bool DeleteLocationAction(int locationId)
          {
               try
               {
                    LocationDbTable locationToDelete;

                    using (var db = new LocationContext())
                    {
                         locationToDelete = db.Locations.FirstOrDefault(l => l.Id == locationId);
                    }

                    if (locationToDelete == null)
                    {
                         return false;
                    }

                    // Șterge imaginea dacă există
                    if (!string.IsNullOrEmpty(locationToDelete.ImagePath))
                    {
                         DeleteLocationImage(locationToDelete.ImagePath);
                    }

                    // Șterge din baza de date
                    using (var db = new LocationContext())
                    {
                         var locationEntity = db.Locations.FirstOrDefault(l => l.Id == locationId);
                         if (locationEntity != null)
                         {
                              db.Locations.Remove(locationEntity);
                              db.SaveChanges();
                         }
                    }

                    return true;
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in DeleteLocationAction: {ex.Message}");
                    return false;
               }
          }

          internal LocationData GetLocationByIdAction(int locationId)
          {
               try
               {
                    using (var db = new LocationContext())
                    {
                         var locationItem = db.Locations.FirstOrDefault(l => l.Id == locationId);
                         if (locationItem != null)
                         {
                              return new LocationData
                              {
                                   Id = locationItem.Id,
                                   Name = locationItem.Name,
                                   Description = locationItem.Description,
                                   Address = locationItem.Address,
                                   Phone = locationItem.Phone,
                                   ImagePath = locationItem.ImagePath
                              };
                         }
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetLocationByIdAction: {ex.Message}");
               }

               return null;
          }

          internal List<LocationData> GetAllLocationsAction()
          {
               var locations = new List<LocationData>();

               try
               {
                    using (var db = new LocationContext())
                    {
                         var locationList = db.Locations
                             .OrderBy(l => l.Name)
                             .ToList();

                         foreach (var locationItem in locationList)
                         {
                              locations.Add(new LocationData
                              {
                                   Id = locationItem.Id,
                                   Name = locationItem.Name,
                                   Description = locationItem.Description,
                                   Address = locationItem.Address,
                                   Phone = locationItem.Phone,
                                   ImagePath = locationItem.ImagePath
                              });
                         }
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetAllLocationsAction: {ex.Message}");
               }

               return locations;
          }

          private void DeleteLocationImage(string imagePath)
          {
               try
               {
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                         string physicalPath = HttpContext.Current.Server.MapPath(imagePath);
                         if (File.Exists(physicalPath))
                         {
                              File.Delete(physicalPath);
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