using _10717proiect.BusinessLogic.Core;
using _10717proiect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.BL_Struct
{
    class SessionBL : UserAPI, ISession
    {

        public bool InvalidateUserSession(string sessionKey)
        {
            return InvalidateUserSessionAction(sessionKey);
        }
    }
}
