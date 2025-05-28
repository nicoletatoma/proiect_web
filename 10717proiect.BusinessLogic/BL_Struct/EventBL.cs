using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _10717proiect.BusinessLogic.Core;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.Event;

namespace _10717proiect.BusinessLogic.BLStruct
{
    public class EventBL : EventAPI, IEvent
    {
        public string CreateEventLogic(LocationData data)
        {
            return EventCreateLogic(data);
        }

        public IEnumerable<EventDbTable> GetAllEvents()
        {
            return GetAllEventsLogic();
        }
    }
}