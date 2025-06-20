using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _10717proiect.Models.Cart
{
     public class CartItem
     {
          public int EventId { get; set; }
          public string EventName { get; set; }
          public string EventImage { get; set; }
          public DateTime EventDate { get; set; }
          public string EventLocation { get; set; }
          public string EventCategory { get; set; }
          public decimal Price { get; set; }
          public int Quantity { get; set; }
          public DateTime AddedAt { get; set; }

          // Proprietăți calculate
          public decimal TotalPrice => Price * Quantity;

          public string FormattedEventDate => EventDate.ToString("dd MMMM yyyy, HH:mm");

          public string FormattedPrice => Price.ToString("0") + " lei";

          public string FormattedTotalPrice => TotalPrice.ToString("0") + " lei";
     }
}