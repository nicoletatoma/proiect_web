using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _10717proiect.Domain.Model.Event;

namespace _10717proiect.BusinessLogic.Interfaces
{
    public interface IEvent
    {
        string CreateEventLogic(EventDataModel data);
        IEnumerable<EventDbTable> GetAllEvents();
    }
}
