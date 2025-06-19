using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _10717proiect.Models.Location
{
     public class LocationViewModel
     {
          public int Id { get; set; }

          [Required(ErrorMessage = "Numele locației este obligatoriu")]
          [StringLength(200, ErrorMessage = "Numele locației nu poate depăși 200 de caractere")]
          [Display(Name = "Numele Locației")]
          public string Name { get; set; }

          [Required(ErrorMessage = "Adresa este obligatorie")]
          [StringLength(500, ErrorMessage = "Adresa nu poate depăși 500 de caractere")]
          [Display(Name = "Adresa")]
          public string Address { get; set; }

          [Display(Name = "Imaginea Locației")]
          public HttpPostedFileBase ImageFile { get; set; }

          public string ImagePath { get; set; }

          // Păstrăm proprietățile pentru compatibilitate dar nu le folosim în form
          public string Description { get; set; }
          public string Phone { get; set; }
     }
}