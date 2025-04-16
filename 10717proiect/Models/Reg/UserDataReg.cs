using _10717proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _10717proiect.Models.Reg
{
    public class UserDataReg
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public URole Level { get; set; }
    }
}