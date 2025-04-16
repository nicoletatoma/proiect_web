using _10717proiect.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Core
{
    public class UserAPI
    {
         

          //metodele entitatilor se termina cu action
          internal bool ValidateSessionIdAction(string id)
        {
            return true;
        }


        //-----------------------------Search--------------------------------

        public string UserSearchLogic(SearchEv eveniment)
        {


               //cautare dupa nume eveniment,    implementare cod


               return "cautare-cuv";
        }
    }
}
