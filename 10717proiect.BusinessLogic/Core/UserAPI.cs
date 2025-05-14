using _10717proiect.BusinessLogic.DBContext.Seed;
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

            return "token-key";
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
