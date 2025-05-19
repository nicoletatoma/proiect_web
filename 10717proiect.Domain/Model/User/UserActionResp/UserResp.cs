using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.Model.User.UserActionResp
{
    public class UserResp
    {
        public bool Status { get; set; }
        public string Error { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
