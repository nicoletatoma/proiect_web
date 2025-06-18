using _10717proiect.BusinessLogic.Core;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Enums;
using _10717proiect.Domain.Model.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _10717proiect.BusinessLogic.BLStruct
{
     public class EventManagerBL : EventAPI, IEventManager
     {
          public Task<bool> CreateEvent(EventData eventData, HttpPostedFileBase imageFile, int userId)
          {
               return CreateEventLogic(eventData, imageFile, userId);
          }

          public bool UpdateEvent(EventData eventData, HttpPostedFileBase imageFile, int userId)
          {
               return UpdateEventLogic(eventData, imageFile, userId);
          }

          public bool DeleteEvent(int eventId, int userId)
          {
               return DeleteEventLogic(eventId, userId);
          }

          public bool UpdateEventStatus(int eventId, EventStatus newStatus, int userId)
          {
               return UpdateEventStatusLogic(eventId, newStatus, userId);
          }

          public List<EventData> GetUserEvents(int userId)
          {
               return GetUserEventsLogic(userId);
          }

          public EventData GetEventById(int eventId, int userId)
          {
               return GetEventByIdLogic(eventId, userId);
          }

          public List<EventData> GetPublishedEvents()
          {
               return GetPublishedEventsLogic();
          }

          public List<EventData> GetEventsByCategory(string category)
          {
               return GetEventsByCategoryLogic(category);
          }

          public List<EventData> GetEventsByLocation(string location)
          {
               return GetEventsByLocationLogic(location);
          }
     }
}