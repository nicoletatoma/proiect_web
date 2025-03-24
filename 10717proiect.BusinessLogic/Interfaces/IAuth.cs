using _10717proiect.BusinessLogic.Core;
using _10717proiect.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Interfaces
{
    public interface IAuth
    {
        string UserAuthLogic(UserLoginDTO data);
    }
}
