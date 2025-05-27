using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Model.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Core
{
    public class AdminAPI
    {
        //----------------------- ADMIN ----------------------------
        public List<UserAllData> GetAllUsersAction()
        {
            using (var db = new UserContext())
            {
                return db.Users
                    .Select(u => new UserAllData
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Email = u.Email,
                        Address = u.Address,
                        Phone = u.Phone,
                        RequestTime = u.RegistartionDateTime,
                        UserRole = u.Level
                    })
                    .ToList();
            }
        }

        public bool EditUserAction(UserAllData data)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == data.Id);

                if (user != null)
                {
                    user.Username = data.Username;
                    user.Email = data.Email;
                    user.Level = data.UserRole;
                    user.Address = data.Address;
                    user.Phone = data.Phone;

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        public List<UserAllData> SearchUserAction(string userData)
        {
            if (string.IsNullOrWhiteSpace(userData))
            {
                return new List<UserAllData>();
            }

            var lowerSearchTerm = userData.ToLower();

            using (var db = new UserContext())
            {
                var users = db.Users
                    .Where(u => u.Username.ToLower().Contains(lowerSearchTerm) ||
                                u.Email.ToLower().Contains(lowerSearchTerm) ||
                                u.Id.ToString().Contains(lowerSearchTerm))
                    .Select(u => new UserAllData
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Email = u.Email,
                        Address = u.Address,
                        Phone = u.Phone,
                        UserRole = u.Level
                    })
                    .ToList();

                return users;


            }

        }


    }
}
