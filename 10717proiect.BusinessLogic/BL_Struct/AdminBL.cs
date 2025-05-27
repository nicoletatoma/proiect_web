using _10717proiect.BusinessLogic.Core;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.BL_Struct
{
    public class AdminBL : AdminAPI, IAdmin
    {
        public List<UserAllData> GetAllUsers()
        {
            return GetAllUsersAction();
        }

        public bool EditUser(UserAllData userData)
        {
            return EditUserAction(userData);
        }

        public List<UserAllData> SearchUser(string userData)
        {
            return SearchUserAction(userData);
        }

      
    }
}
