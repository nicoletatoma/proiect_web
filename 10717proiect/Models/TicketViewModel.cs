using System;

namespace _10717proiect.Models.Tickets
{
    public class TicketViewModel
    {
        public string TicketId { get; set; }
        public string EventName { get; set; }
        public string EventImage { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        // Proprietăți calculate
        public decimal TotalPrice => Price * Quantity;
        public bool IsExpired => EventDate < DateTime.Now;
        public bool IsUpcoming => EventDate > DateTime.Now;
        public string StatusClass
        {
            get
            {
                switch (Status?.ToLower())
                {
                    case "valabil":
                        return "badge-success";
                    case "expirat":
                        return "badge-secondary";
                    case "anulat":
                        return "badge-danger";
                    default:
                        return "badge-primary";
                }
            }
        }
    }
}