using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.Model.User
{
    //object common for BL and presentation layer
     public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserIP { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
