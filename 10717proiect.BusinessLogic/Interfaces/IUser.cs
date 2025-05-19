using _10717proiect.Domain.Model.User.UserActionResp;
using _10717proiect.Domain.User.Reg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _10717proiect.BusinessLogic.Interfaces
{
    public interface IUser
    {
        UserResp GetUserByCookie(string sessionKey);
        UserRegDataResp RegisterUserAction(UserRegDTO local);
    }
}
