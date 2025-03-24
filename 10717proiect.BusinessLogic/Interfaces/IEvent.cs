using _10717proiect.Domain.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Interfaces
{
    public interface IEvent
    {
        string UserEventLogic(EventDTO data);
        //string DeleteEventLogic(int id);
        //string UpdateEventLogic(UpdateEventDTO data);
        //string GetEventLogic(int id);
        //string GetAllEventsLogic();
    }
}
