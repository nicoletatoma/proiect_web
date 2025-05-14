using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.Model.Event
{
    [Table("EventDbTables")]
    public class EventDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Numele evenimentului")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Descriere")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Data evenimentului")]
        public DateTime EventDate { get; set; }

        [Required]
        [Display(Name = "Locație")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Preț")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Imagine")]
        public string ImagePath { get; set; }

        [Display(Name = "Data creării")]
        public DateTime CreatedAt { get; set; }
    }
}
