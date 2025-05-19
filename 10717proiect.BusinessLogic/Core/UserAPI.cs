using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Model.Session;
using _10717proiect.Domain.Model.User;
using _10717proiect.Domain.Model.User.UserActionResp;
using _10717proiect.Domain.User;
using _10717proiect.Domain.User.Reg;
using _10717proiect.Helpers.RegFlow;
using _10717proiect.Helpers.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _10717proiect.BusinessLogic.Core
{
    public class UserAPI
    {
        ////metodele entitatilor se termina cu action
        //internal bool ValidateSessionIdAction(string id)
        //{
        //    return true;
        //}


        //---------------AUTH----------------
        internal UserResp UserAuthLogicAction(UserLoginDTO data)
        {
            UDbTable user;

            try
            {
                var passHashed = LoginRegHelper.HashGen(data.Password);

                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(
                    u => u.Email == data.Email && u.Password == passHashed);
                }

            }
            catch (Exception ex)
            {
                return new UserResp()
                {
                    Status = false,
                    Error = ex.Message
                };
            }


            if (user != null)
            {

                return new UserResp()
                {
                    Status = false,
                    Error = "no user found"
                };

            }

           
            return new UserResp()
            {
                Status = true,
                Error = string.Empty,
                UserId = 1
            };

        }

        internal UserResp GetUserByCookieAction(string cookieKey)
        {
            USessionDbTable session;
            UDbTable user;
            using (var db = new SessionContext())
            {
                session = db.Session.FirstOrDefault(s => s.Cookie.Contains(cookieKey));
            }

            if (session != null)
            {
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Id == session.UserId);
                }
                if (user != null)
                {
                    return new UserResp { Status = true, UserId = user.Id,
                        Username = user.Username, Level = user.Level }; 
                }
               
            }

            return new UserResp { Status = false };
        }
        internal UserCookieResp GenerateCookieByUserAction(int userId)
        {

            var cookieString = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(userId +"[0.0.0.0]" + "ISP: Super Fast Internet")
            };

            var dateTime = DateTime.Now;

            USessionDbTable session;

            using (var db = new SessionContext())
            {

                session = db.Session.FirstOrDefault(u => u.UserId == userId);
            }

            if (session != null)
            {
                //UPDATE TABLE

                session.Cookie = cookieString.ToString();
                session.IsValidTime = dateTime;

                using (var db = new SessionContext())
                {
                    db.Entry(session).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                //INSERT TABLE

                session = new USessionDbTable()
                {
                    UserId = userId,
                    Cookie = cookieString.Value,
                    IsValidTime = dateTime
                };

                using (var db = new SessionContext())
                {
                    db.Session.Add(session);
                    db.SaveChanges();
                }
            }


            return new UserCookieResp() { UserId = userId, Cookie = cookieString, ValidUntil = dateTime };
        }

        //using (var db = new UserContext())
        //{
        //     var user = db.Users.FirstOrDefault(u => u.Email == data.Email);

        //     if (user != null)
        //     {
        //          // Verifică parola
        //          if (user.Password == data.Password)
        //          {
        //               // Updatează ultima logare
        //               user.LastLoginDateTime = DateTime.Now;
        //               db.SaveChanges();

        //               // Returnează un token temporar sau ID sesiune
        //               return "token-key"; // Poți returna user.Id.ToString() dacă vrei
        //          }
        //          else
        //          {
        //               // Parolă greșită - în practică, ai returna un mesaj de eroare
        //               return null;
        //          }
        //     }
        //     else
        //     {
        //          // Creează un nou cont
        //          var u_data = new UDbTable()
        //          {
        //               Email = data.Email,
        //               Password = data.Password,
        //               LastLoginDateTime = DateTime.Now,
        //               RegistartionDateTime = DateTime.Now,

        //               // Adăugăm câmpuri obligatorii
        //               Username = "default_user",         // sau ceva preluat din `data`
        //               Address = "N/A",
        //               Phone = "0000000000",
        //               Level = Domain.Enums.URole.User,  // presupunem că ai enumul `URole`
        //               UserIP = "0.0.0.0"
        //          };


        //          db.Users.Add(u_data);
        //          db.SaveChanges();

        //          return "token-key"; // Sau u_data.Id.ToString()
        //     }
        //}




        //---------------Reg---------------

        //  public bool UserRegisterDTO(UserRegDTO data)
        //{

        //    Console.WriteLine($"User {data.Username} registered!");
        //    return true;
        //}

        internal UserRegDataResp SetRegisterUserAction(UserRegDTO local)
        {
            UDbTable user;
           
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Username == local.Username && u.Email == local.Email);

                }
            

            if (user != null)
            {
                return new UserRegDataResp()
                {
                    Status = false,
                    Error = "This Username or Email is already registered"
                };

            }

            var passHashed = LoginRegHelper.HashGen(local.Password);

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

            return new UserRegDataResp() {  Status = true };
        }
      

    }
}
