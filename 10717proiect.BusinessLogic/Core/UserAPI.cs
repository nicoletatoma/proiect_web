using _10717proiect.BusinessLogic.BL_Struct;
using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.BusinessLogic.Interfaces;

using _10717proiect.Domain.Model.User;
using _10717proiect.Domain.Model.User.Auth;
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


          //---------------AUTH----------------
          public string UserAuthLogicAction(UserLoginDTO data)
          {


               ULogDbTable user;

               using (var db = new UserContext())
               {
                    user = db.Users.FirstOrDefault(u => u.Email == data.Email);
               }

               if (user != null)
               {
                    

               }

               var u_data = new ULogDbTable()
               {
                    Email = data.Email,
                    Password = data.Password,
                    LastLoginDateTime = DateTime.Now,
                    RegistartionDateTime = DateTime.Now,
                   
               };


               using (var db = new UserContext())
               {
                    db.Users.Add(u_data);
                    db.SaveChanges();
               }

               return "token-key";
          }

         


          //-----------------------------Search--------------------------------

          //  public SearchBL UserSearchLogicAction(SearchEvenimentAction cautare)
          //{

          //       EvTable evenim;

          //       //cautare dupa nume eveniment,    implementare cod
          //       using (var db = new SearchContext())
          //       {
          //            evenim = db.Search.FirstOrDefault(x => x.NumeEveniment == cautare.NumeEveniment);                
          //       }




          //            return new EvTable();
          //}
     }
}
