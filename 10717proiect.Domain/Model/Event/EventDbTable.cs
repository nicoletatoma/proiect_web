using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _10717proiect.Domain.Enums;

namespace _10717proiect.Domain.Model.Event
{
    [Table("EventDbTables")]
     public class EventDbTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Required]
          [Display(Name = "User Id")]
          public int userId { get; set; }

          [Required]
          [Display(Name = "Event Name")]
          [StringLength(200)]
          public string eventName { get; set; }

          [Display(Name = "Description")]
          [StringLength(1000)]
          public string eventDescription { get; set; }

          [Required]
          [Display(Name = "Event Date")]
          public DateTime eventDate { get; set; }

          [Required]
          [Display(Name = "Location")]
          [StringLength(100)]
          public string eventLocation { get; set; }

          [Required]
          [Display(Name = "Category")]
          [StringLength(50)]
          public string eventCategory { get; set; }

          [Required]
          [Display(Name = "Price")]
          [Column(TypeName = "decimal(10,2)")]
          public decimal eventPrice { get; set; }

          [Display(Name = "Event Image")]
          [StringLength(500)]
          public string eventImage { get; set; }

          [Required]
          [Display(Name = "Status")]
          public EventStatus eventStatus { get; set; }

          [Display(Name = "Created At")]
          public DateTime createdAt { get; set; }

          [Display(Name = "Updated At")]
          public DateTime updatedAt { get; set; }
     }
}
