using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Model.Events;
using _10717proiect.Domain.Model.User;
using _10717proiect.Domain.Model.User.Reg;
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
        //---------------REGISTRATION----------------
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
                    Error = "An Usernam is already registered",

                };

            }

            var u_data = new UDbTable()
            {
                Username = local.Username,
                Password = local.Password,
                Email = local.Email,
                Phone = local.Phone,
                Address = local.Address,
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

        //---------------AUTH----------------
        public string UserAuthLogicAction(UserLoginDTO data)
        {
            using (var db = new UserContext())
            { 
                var user = db.Users.FirstOrDefault(u => u.Username == data.Username);
                var auth = user;
            }
                return "token-key";
        }

        //---------------EVENT----------------
        public string UserEventLogicAction(EventDTO data)
        {
            return "event-key";
        }
    }

}
