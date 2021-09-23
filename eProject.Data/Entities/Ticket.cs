using eProject.Data.Enum;
using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public partial class Ticket
    {
        public Ticket()
        {
            TicketDetails = new HashSet<TicketDetail>();
        }

        public Guid TicketId { get; set; }
        public Guid FlightId { get; set; }
        public Guid UserId { get; set; }
        public int TicketStatus { get; set; }
        public int TicketType { get; set; }
        public DateTime BookingDate { get; set; }
        public string PaymentMethod { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public double TotalPrice { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual AppUser AppUser { get; set; } 
        public virtual ICollection<TicketDetail> TicketDetails { get; set; }
    }
}
