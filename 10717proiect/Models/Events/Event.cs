using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _10717proiect.Models.Events
{
	public class Event
	{
        public string Nume { get; set; }
        public string Categorie { get; set; }
        public decimal Pret { get; set; }
        public DateTime Data { get; set; }
        public string Locatie { get; set; }
        public string Descriere { get; set; }
        public string ImagineURL { get; set; }
    }
}