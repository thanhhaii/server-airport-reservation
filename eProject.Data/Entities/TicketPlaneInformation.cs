using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public partial class TicketPlaneInformation
    {
        public TicketPlaneInformation()
        {
            DiscountTickets = new HashSet<DiscountTicket>();
        }

        public int TicketPlaneInformationId { get; set; }
        public string FlightId { get; set; }
        public decimal PriceTicketFirstClass { get; set; }
        public decimal PriceTicketBusiness { get; set; }
        public decimal PriceTicketPremiumEconomy { get; set; }
        public decimal PriceTicketEconomy { get; set; }

        public virtual ICollection<DiscountTicket> DiscountTickets { get; set; }
    }
}
