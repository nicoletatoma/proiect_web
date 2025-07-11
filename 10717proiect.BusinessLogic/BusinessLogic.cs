﻿using _10717proiect.BusinessLogic.BL_Struct;
using _10717proiect.BusinessLogic.BLStruct;
using _10717proiect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IAuth GetAuthBL() 
        { 
            return new AuthBL();
        }

        public IUser GetUserBL()
        {
            return new UserBL();
        }
          public IEventManager GetEventManagerBL()
          {
               return new EventManagerBL();
          }

          public IComment GetCommentBL()
        {
            return new CommentBL();
        }

        public IAdmin GetAdminBL()
        {
            return new AdminBL();
        }

        public ILocation GetLocationBL()
        {
            return new LocationBL();
        }

    }
}
