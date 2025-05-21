using _10717proiect.BusinessLogic.Core;
using _10717proiect.Domain.Model.User;
using _10717proiect.Domain.Model.User.UserActionResp;
using _10717proiect.Domain.User.Reg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Interfaces
{
    public interface IAuth
    {
        UserCookieResp GeneratCookieByUser(int id);
        UserResp UserAuthLogic(UserLoginDTO data);
        bool InvalidateUserSession(string sessionKey);
    }
}
