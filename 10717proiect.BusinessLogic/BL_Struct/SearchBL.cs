using _10717proiect.BusinessLogic.Core;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.BL_Struct
{
    internal class SearchBL : UserAPI, ISearch
    {
        public string UserSearchLogic(SearchEv eveniment)
        {
            return UserSearchLogic(eveniment);
        }
    }
}
