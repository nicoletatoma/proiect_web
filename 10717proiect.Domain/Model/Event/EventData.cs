using _10717proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.Model.Event
{
    public class EventData
    {
          public int Id { get; set; }
          public int userId { get; set; }
          public string eventName { get; set; }
          public string eventDescription { get; set; }
          public DateTime eventDate { get; set; }
          public string eventLocation { get; set; }
          public string eventCategory { get; set; }
          public decimal eventPrice { get; set; }
          public string eventImage { get; set; }
          public EventStatus eventStatus { get; set; }
          public DateTime createdAt { get; set; }
          public DateTime updatedAt { get; set; }
     }
}
