using _10717proiect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _10717proiect.Models.Event
{
     public class EventListViewModel
     {
          public int Id { get; set; }
          public string eventName { get; set; }
          public string eventDescription { get; set; }
          public DateTime eventDate { get; set; }
          public string eventLocation { get; set; }
          public string eventCategory { get; set; }
          public decimal eventPrice { get; set; }
          public string eventImage { get; set; }
          public EventStatus eventStatus { get; set; }
          public DateTime createdAt { get; set; }
          public DateTime updatedAt { get; set; }
     }
     public class EventViewModel
     {
          public int Id { get; set; }

          [Required(ErrorMessage = "Numele evenimentului este obligatoriu")]
          [StringLength(200, ErrorMessage = "Numele evenimentului nu poate depăși 200 de caractere")]
          [Display(Name = "Numele Evenimentului")]
          public string eventName { get; set; }

          [StringLength(1000, ErrorMessage = "Descrierea nu poate depăși 1000 de caractere")]
          [Display(Name = "Descriere")]
          public string eventDescription { get; set; }

          [Required(ErrorMessage = "Data evenimentului este obligatorie")]
          [Display(Name = "Data Evenimentului")]
          public DateTime eventDate { get; set; }

          [Required(ErrorMessage = "Locația este obligatorie")]
          [Display(Name = "Locația")]
          public string eventLocation { get; set; }

          [Required(ErrorMessage = "Categoria este obligatorie")]
          [Display(Name = "Categoria")]
          public string eventCategory { get; set; }

          [Required(ErrorMessage = "Prețul este obligatoriu")]
          [Range(0, double.MaxValue, ErrorMessage = "Prețul trebuie să fie mai mare sau egal cu 0")]
          [Display(Name = "Preț")]
          public decimal eventPrice { get; set; }

          [Display(Name = "Imaginea Evenimentului")]
          public HttpPostedFileBase eventImageFile { get; set; }

          public string eventImage { get; set; }

          [Display(Name = "Status")]
          public EventStatus eventStatus { get; set; }

          [Display(Name = "Data creării")]
          public DateTime createdAt { get; set; }
          [Display(Name = "Data actualizarii")]
          public DateTime updatedAt { get; set; }
     }
}