using _10717proiect.Domain.Enums;
using _10717proiect.Domain.Model.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _10717proiect.BusinessLogic.Interfaces
{
     public interface IEventManager
     {
          Task<bool> CreateEvent(EventData eventData, HttpPostedFileBase imageFile, int userId);

          bool UpdateEvent(EventData eventData, HttpPostedFileBase imageFile, int userId);

          bool DeleteEvent(int eventId, int userId);

          bool UpdateEventStatus(int eventId, EventStatus newStatus, int userId);

          List<EventData> GetUserEvents(int userId);

          EventData GetEventById(int eventId, int userId);

          List<EventData> GetPublishedEvents();

          List<EventData> GetEventsByCategory(string category);

          List<EventData> GetEventsByLocation(string location);
     }
}
