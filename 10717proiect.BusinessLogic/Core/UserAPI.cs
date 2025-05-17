using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Model.User;
using _10717proiect.Domain.User;
using _10717proiect.Domain.User.Reg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
               using (var db = new UserContext())
               {
                    var user = db.Users.FirstOrDefault(u => u.Email == data.Email);

                    if (user != null)
                    {
                         // Verifică parola
                         if (user.Password == data.Password)
                         {
                              // Updatează ultima logare
                              user.LastLoginDateTime = DateTime.Now;
                              db.SaveChanges();

                              // Returnează un token temporar sau ID sesiune
                              return "token-key"; // Poți returna user.Id.ToString() dacă vrei
                         }
                         else
                         {
                              // Parolă greșită - în practică, ai returna un mesaj de eroare
                              return null;
                         }
                    }
                    else
                    {
                         // Creează un nou cont
                         var u_data = new UDbTable()
                         {
                              Email = data.Email,
                              Password = data.Password,
                              LastLoginDateTime = DateTime.Now,
                              RegistartionDateTime = DateTime.Now,

                              // Adăugăm câmpuri obligatorii
                              Username = "default_user",         // sau ceva preluat din `data`
                              Address = "N/A",
                              Phone = "0000000000",
                              Level = Domain.Enums.URole.User,  // presupunem că ai enumul `URole`
                              UserIP = "0.0.0.0"
                         };


                         db.Users.Add(u_data);
                         db.SaveChanges();

                         return "token-key"; // Sau u_data.Id.ToString()
                    }
               }
          }


          //---------------Reg---------------

          public bool UserRegisterDTO(UserRegDTO data)
        {

            Console.WriteLine($"User {data.Username} registered!");
            return true;
        }

        internal UserRegDataResp SetRegisterUserAction(UserRegDTO local)
        {
            UDbTable user;

            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Username == local.Username);

            }

            if (user != null)
            {
                return new UserRegDataResp()
                {
                    Status = false,
                    Error = "An Username is already registered"
                };

            }

            var u_data = new UDbTable()
            {
                Address = local.Address,
                Email = local.Email,
                Password = local.Password,
                Phone = local.Phone,
                Username = local.Username,
                LastLoginDateTime = DateTime.Now,
                RegistartionDateTime = DateTime.Now,
                Level = Domain.Enums.URole.User,
                UserIP = "0.0.0.0"
            };

            using (var db = new UserContext())
            {
                db.Users.Add(u_data);
                db.SaveChanges();
            }

            return new UserRegDataResp() { Status = true };
        }
      

    }
}
