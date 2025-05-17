using _10717proiect.BusinessLogic.Core;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.User;
using _10717proiect.Domain.Model.User.UserActionResp;
using _10717proiect.Domain.User.Reg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.BL_Struct
{
    public class AuthBL : UserAPI, IAuth
    {
        public UserCookieResp GeneratCookieByUser(int id)
        {
            return GeneratCookieByUserAction(id);
        }

        public UserResp UserAuthLogic(UserLoginDTO data)
        {
            return UserAuthLogicAction(data);
        }
    }
}
