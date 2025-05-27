using _10717proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.Model.Admin
{
    public class UserAllData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public URole UserRole { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime? RequestTime { get; set; }
    }
}
