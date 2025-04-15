using _10717proiect.Domain.Model.Events;
using _10717proiect.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Core
{
    public class UserAPI
    {
        //metodele entitatilor se termina cu action
        internal bool ValidateSessionIdAction(string id)
        {
            return true;
        }

        //---------------AUTH----------------
        public string UserAuthLogicAction(UserLoginDTO data)
        {

            return "token-key";
        }

        //---------------EVENT----------------
        public string UserEventLogicAction(EventDTO data)
        {
            return "event-key";
        }
    }

}
