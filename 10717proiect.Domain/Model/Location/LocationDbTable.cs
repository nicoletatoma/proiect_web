using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.Model.Location
{
     [Table("LocationDbTable")]
     public class LocationDbTable
     {
          
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Required]
          [Display(Name = "Numele loactiei")]
          public string Name { get; set; }

          [Required]
          [Display(Name = "Descriere")]
          public string Description { get; set; }

          [Required]
          [Display(Name = "Adresa")]
          public string Address { get; set; }

          [Required]
          [Display(Name = "Telefon")]
          public string Phone { get; set; }

          [Required]
          [Display(Name = "Imagine")]
          public string ImagePath { get; set; }

          
     }
}
