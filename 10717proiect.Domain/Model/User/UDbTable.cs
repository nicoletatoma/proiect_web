using _10717proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.Model.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 caracters.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "email")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The minimum chars is 5 and Maximum is 30.")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string UserIP { get; set; }

        [Display(Name = "reg_dt")]
        public DateTime RegistartionDateTime { get; set; }

        [Display(Name = "login_dt")]
        public DateTime LastLoginDateTime { get; set; }

        public URole Level { get; set; }



    }
}
