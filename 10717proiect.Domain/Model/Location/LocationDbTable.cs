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
          [Display(Name = "Numele locației")]
          [StringLength(200)]
          public string Name { get; set; }

          [Display(Name = "Descriere")]
          [StringLength(1000)]
          public string Description { get; set; } // Nu mai e Required

          [Required]
          [Display(Name = "Adresa")]
          [StringLength(500)]
          public string Address { get; set; }

          [Display(Name = "Telefon")]
          [StringLength(20)]
          public string Phone { get; set; } // Nu mai e Required

          [Display(Name = "Imagine")]
          [StringLength(500)]
          public string ImagePath { get; set; }
     }
}