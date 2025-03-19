using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Interfaces
{
    public interface ISession
    {
        //doar structura metodei fara logica, fara implementare
        bool ValidateSessionId(string id);
    }
}
