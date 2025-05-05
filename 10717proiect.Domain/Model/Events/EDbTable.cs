using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.Domain.Model.Events
{
    public class EDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Display(Name = "nume")]
        public string Nume { get; set; }

        [Display(Name = "categorie")]
        public string Categorie { get; set; }

        [Display(Name = "price")]
        public decimal Pret { get; set; }

        public DateTime Data { get; set; }
        public string Locatie { get; set; }
        public string Descriere { get; set; }
        public string ImagineURL { get; set; }
    }
}
