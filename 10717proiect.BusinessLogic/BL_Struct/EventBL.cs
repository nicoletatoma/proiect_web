using _10717proiect.BusinessLogic.Core;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.BL_Struct
{
    internal class EventBL : UserAPI, IEvent
    {
      
        public string UserEventLogic(EventDTO data)
        {
            return UserEventLogicAction(data);
        }
    }
}
