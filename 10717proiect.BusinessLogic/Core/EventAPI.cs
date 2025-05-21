using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Model.Event;

namespace _10717proiect.BusinessLogic.Core
{
    public class EventAPI
    {
        public string EventCreateLogic(EventDataModel data)
        {
            if (data == null ||
                data.EventDate == DateTime.MinValue ||
                string.IsNullOrEmpty(data.Name) ||
                string.IsNullOrEmpty(data.Description) ||
                string.IsNullOrEmpty(data.Location))
            {
                return null;
            }

            using (var dbContext = new EventContext())
            {
                var newEvent = new EventDbTable
                {
                    Name = data.Name,
                    Description = data.Description,
                    EventDate = data.EventDate,
                    Location = data.Location,
                    CategoryId = data.CategoryId,
                    Price = data.Price,
                    ImagePath = data.ImagePath,
                    CreatedAt = DateTime.Now
                };
                dbContext.Events.Add(newEvent);
                dbContext.SaveChanges();
                return "Event created successfully!";
            }



        }
        public IEnumerable<EventDbTable> GetAllEventsLogic()
        {
            using (var dbContext = new EventContext())
            {
                return dbContext.Events.ToList();
            }
        }
    }      
}