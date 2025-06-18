using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Enums;
using _10717proiect.Domain.Model.Event;
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
     public class EventAPI
     {
          internal async Task<bool> CreateEventLogic(EventData eventData, HttpPostedFileBase imageFile, int userId)
          {
               try
               {
                    // Validare date
                    if (string.IsNullOrEmpty(eventData.eventName) ||
                        string.IsNullOrEmpty(eventData.eventLocation) ||
                        string.IsNullOrEmpty(eventData.eventCategory) ||
                        eventData.eventPrice < 0)
                    {
                         return false;
                    }

                    string imagePath = null;

                    // Procesare imagine dacă este furnizată
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                         imagePath = await SaveEventImage(imageFile, eventData.eventName);
                         if (imagePath == null)
                         {
                              return false; // Eroare la salvarea imaginii
                         }
                    }

                    // Creare eveniment în baza de date
                    var newEvent = new EventDbTable
                    {
                         userId = userId,
                         eventName = eventData.eventName,
                         eventDescription = eventData.eventDescription,
                         eventDate = eventData.eventDate,
                         eventLocation = eventData.eventLocation,
                         eventCategory = eventData.eventCategory,
                         eventPrice = eventData.eventPrice,
                         eventImage = imagePath,
                         eventStatus = eventData.eventStatus,
                         createdAt = DateTime.Now,
                         updatedAt = DateTime.Now
                    };

                    using (var db = new EventContext())
                    {
                         db.Events.Add(newEvent);
                         db.SaveChanges();
                    }

                    return true;
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in CreateEventLogic: {ex.Message}");
                    return false;
               }
          }

          internal bool UpdateEventLogic(EventData eventData, HttpPostedFileBase imageFile, int userId)
          {
               try
               {
                    EventDbTable existingEvent;

                    using (var db = new EventContext())
                    {
                         existingEvent = db.Events.FirstOrDefault(e => e.Id == eventData.Id && e.userId == userId);
                    }

                    if (existingEvent == null)
                    {
                         return false; // Evenimentul nu există sau nu aparține utilizatorului
                    }

                    // Procesare imagine nouă dacă este furnizată
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                         var newImagePath = SaveEventImage(imageFile, eventData.eventName).Result;
                         if (newImagePath != null)
                         {
                              // Șterge imaginea veche dacă există
                              if (!string.IsNullOrEmpty(existingEvent.eventImage))
                              {
                                   DeleteEventImage(existingEvent.eventImage);
                              }
                              existingEvent.eventImage = newImagePath;
                         }
                    }

                    // Actualizare date
                    existingEvent.eventName = eventData.eventName;
                    existingEvent.eventDescription = eventData.eventDescription;
                    existingEvent.eventDate = eventData.eventDate;
                    existingEvent.eventLocation = eventData.eventLocation;
                    existingEvent.eventCategory = eventData.eventCategory;
                    existingEvent.eventPrice = eventData.eventPrice;
                    existingEvent.eventStatus = eventData.eventStatus;
                    existingEvent.updatedAt = DateTime.Now;

                    using (var db = new EventContext())
                    {
                         db.Entry(existingEvent).State = EntityState.Modified;
                         db.SaveChanges();
                    }

                    return true;
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in UpdateEventLogic: {ex.Message}");
                    return false;
               }
          }

          internal bool DeleteEventLogic(int eventId, int userId)
          {
               try
               {
                    EventDbTable eventToDelete;

                    using (var db = new EventContext())
                    {
                         eventToDelete = db.Events.FirstOrDefault(e => e.Id == eventId && e.userId == userId);
                    }

                    if (eventToDelete == null)
                    {
                         return false;
                    }

                    // Șterge imaginea dacă există
                    if (!string.IsNullOrEmpty(eventToDelete.eventImage))
                    {
                         DeleteEventImage(eventToDelete.eventImage);
                    }

                    // Șterge din baza de date
                    using (var db = new EventContext())
                    {
                         var eventEntity = db.Events.FirstOrDefault(e => e.Id == eventId);
                         if (eventEntity != null)
                         {
                              db.Events.Remove(eventEntity);
                              db.SaveChanges();
                         }
                    }

                    return true;
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in DeleteEventLogic: {ex.Message}");
                    return false;
               }
          }

          internal bool UpdateEventStatusLogic(int eventId, EventStatus newStatus, int userId)
          {
               try
               {
                    using (var db = new EventContext())
                    {
                         var eventEntity = db.Events.FirstOrDefault(e => e.Id == eventId && e.userId == userId);
                         if (eventEntity != null)
                         {
                              eventEntity.eventStatus = newStatus;
                              eventEntity.updatedAt = DateTime.Now;
                              db.SaveChanges();
                              return true;
                         }
                    }

                    return false;
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in UpdateEventStatusLogic: {ex.Message}");
                    return false;
               }
          }

          internal List<EventData> GetUserEventsLogic(int userId)
          {
               var events = new List<EventData>();

               try
               {
                    using (var db = new EventContext())
                    {
                         var eventList = db.Events
                             .Where(e => e.userId == userId)
                             .OrderByDescending(e => e.createdAt)
                             .ToList();

                         foreach (var eventItem in eventList)
                         {
                              events.Add(new EventData
                              {
                                   Id = eventItem.Id,
                                   userId = eventItem.userId,
                                   eventName = eventItem.eventName,
                                   eventDescription = eventItem.eventDescription,
                                   eventDate = eventItem.eventDate,
                                   eventLocation = eventItem.eventLocation,
                                   eventCategory = eventItem.eventCategory,
                                   eventPrice = eventItem.eventPrice,
                                   eventImage = eventItem.eventImage,
                                   eventStatus = eventItem.eventStatus,
                                   createdAt = eventItem.createdAt,
                                   updatedAt = eventItem.updatedAt
                              });
                         }
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetUserEventsLogic: {ex.Message}");
               }

               return events;
          }

          internal EventData GetEventByIdLogic(int eventId, int userId)
          {
               try
               {
                    using (var db = new EventContext())
                    {
                         var eventItem = db.Events.FirstOrDefault(e => e.Id == eventId && e.userId == userId);
                         if (eventItem != null)
                         {
                              return new EventData
                              {
                                   Id = eventItem.Id,
                                   userId = eventItem.userId,
                                   eventName = eventItem.eventName,
                                   eventDescription = eventItem.eventDescription,
                                   eventDate = eventItem.eventDate,
                                   eventLocation = eventItem.eventLocation,
                                   eventCategory = eventItem.eventCategory,
                                   eventPrice = eventItem.eventPrice,
                                   eventImage = eventItem.eventImage,
                                   eventStatus = eventItem.eventStatus,
                                   createdAt = eventItem.createdAt,
                                   updatedAt = eventItem.updatedAt
                              };
                         }
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetEventByIdLogic: {ex.Message}");
               }

               return null;
          }

          internal List<EventData> GetPublishedEventsLogic()
          {
               var events = new List<EventData>();

               try
               {
                    using (var db = new EventContext())
                    {
                         var eventList = db.Events
                             .Where(e => e.eventStatus == EventStatus.Published && e.eventDate > DateTime.Now)
                             .OrderBy(e => e.eventDate)
                             .ToList();

                         foreach (var eventItem in eventList)
                         {
                              events.Add(new EventData
                              {
                                   Id = eventItem.Id,
                                   userId = eventItem.userId,
                                   eventName = eventItem.eventName,
                                   eventDescription = eventItem.eventDescription,
                                   eventDate = eventItem.eventDate,
                                   eventLocation = eventItem.eventLocation,
                                   eventCategory = eventItem.eventCategory,
                                   eventPrice = eventItem.eventPrice,
                                   eventImage = eventItem.eventImage,
                                   eventStatus = eventItem.eventStatus,
                                   createdAt = eventItem.createdAt,
                                   updatedAt = eventItem.updatedAt
                              });
                         }
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetPublishedEventsLogic: {ex.Message}");
               }

               return events;
          }

          internal List<EventData> GetEventsByCategoryLogic(string category)
          {
               var events = new List<EventData>();

               try
               {
                    using (var db = new EventContext())
                    {
                         var eventList = db.Events
                             .Where(e => e.eventCategory == category && e.eventStatus == EventStatus.Published && e.eventDate > DateTime.Now)
                             .OrderBy(e => e.eventDate)
                             .ToList();

                         foreach (var eventItem in eventList)
                         {
                              events.Add(new EventData
                              {
                                   Id = eventItem.Id,
                                   userId = eventItem.userId,
                                   eventName = eventItem.eventName,
                                   eventDescription = eventItem.eventDescription,
                                   eventDate = eventItem.eventDate,
                                   eventLocation = eventItem.eventLocation,
                                   eventCategory = eventItem.eventCategory,
                                   eventPrice = eventItem.eventPrice,
                                   eventImage = eventItem.eventImage,
                                   eventStatus = eventItem.eventStatus,
                                   createdAt = eventItem.createdAt,
                                   updatedAt = eventItem.updatedAt
                              });
                         }
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetEventsByCategoryLogic: {ex.Message}");
               }

               return events;
          }

          internal List<EventData> GetEventsByLocationLogic(string location)
          {
               var events = new List<EventData>();

               try
               {
                    using (var db = new EventContext())
                    {
                         var eventList = db.Events
                             .Where(e => e.eventLocation == location && e.eventStatus == EventStatus.Published && e.eventDate > DateTime.Now)
                             .OrderBy(e => e.eventDate)
                             .ToList();

                         foreach (var eventItem in eventList)
                         {
                              events.Add(new EventData
                              {
                                   Id = eventItem.Id,
                                   userId = eventItem.userId,
                                   eventName = eventItem.eventName,
                                   eventDescription = eventItem.eventDescription,
                                   eventDate = eventItem.eventDate,
                                   eventLocation = eventItem.eventLocation,
                                   eventCategory = eventItem.eventCategory,
                                   eventPrice = eventItem.eventPrice,
                                   eventImage = eventItem.eventImage,
                                   eventStatus = eventItem.eventStatus,
                                   createdAt = eventItem.createdAt,
                                   updatedAt = eventItem.updatedAt
                              });
                         }
                    }
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error in GetEventsByLocationLogic: {ex.Message}");
               }

               return events;
          }

          private async Task<string> SaveEventImage(HttpPostedFileBase imageFile, string eventName)
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
                    string baseEventsPath = HttpContext.Current.Server.MapPath("~/Content/Images/Events");
                    string userEventsPath = Path.Combine(baseEventsPath, eventName);

                    if (!Directory.Exists(baseEventsPath))
                    {
                         Directory.CreateDirectory(baseEventsPath);
                    }

                    if (!Directory.Exists(userEventsPath))
                    {
                         Directory.CreateDirectory(userEventsPath);
                    }

                    // Generare nume unic pentru fișier
                    string fileExtension = Path.GetExtension(imageFile.FileName);
                    string fileName = $"{Guid.NewGuid()}{fileExtension}";
                    string filePath = Path.Combine(userEventsPath, fileName);

                    // Salvare fișier
                    imageFile.SaveAs(filePath);

                    // Returnare path relativ
                    return $"~/Content/Images/Events/{eventName}/{fileName}";
               }
               catch (Exception ex)
               {
                    System.Diagnostics.Debug.WriteLine($"Error saving event image: {ex.Message}");
                    return null;
               }
          }

          private void DeleteEventImage(string imagePath)
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
                    System.Diagnostics.Debug.WriteLine($"Error deleting event image: {ex.Message}");
               }
          }
     }
}