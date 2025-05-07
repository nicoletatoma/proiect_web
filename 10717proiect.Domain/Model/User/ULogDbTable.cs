using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.Model.User
{
     public class ULogDbTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Required]
          [Display(Name = "email")]
          [StringLength(30, MinimumLength = 5, ErrorMessage = "The minimum chars is 5 and Maximum is 30.")]
          public string Email { get; set; }

          [Required]
          [Display(Name = "password")]
          [StringLength(50, MinimumLength = 4, ErrorMessage = "Password cannot be shorter than 8 caracters.")]
          public string Password { get; set; }



          [Display(Name = "reg_dt")]
          public DateTime RegistartionDateTime { get; set; }

          [Display(Name = "login_dt")]
          public DateTime LastLoginDateTime { get; set; }





     }
}
