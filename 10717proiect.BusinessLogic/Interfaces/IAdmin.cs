using _10717proiect.Domain.Model.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        List<UserAllData> GetAllUsers();
        bool EditUser(UserAllData userData);
        List<UserAllData> SearchUser(string userData);
    }
}
