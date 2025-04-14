using _10717proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "email address")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Email cannot be shorter than 5 characters.")]
        public string Email { get; set; }

        [Display(Name = "phone")]
        public string Phone {  get; set; }

        [Display(Name = "address")]
        public string Address { get; set; }

        [Display(Name = "ip")]
        public string UserIP { get; set; }

        [Display(Name ="reg_dt")]
        public DateTime RegistrationDateTime { get; set; }

        [Display(Name = "login_dt")]
        public DateTime LastLoginGateTime { get; set; }

        [Display(Name = "u_level")]
        public URole Level { get; set; }
    }
}
