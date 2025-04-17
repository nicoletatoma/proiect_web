using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _10717proiect.Models.Event
{
    public class EventDataModelView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}