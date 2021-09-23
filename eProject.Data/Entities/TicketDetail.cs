using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public partial class TicketDetail
    {
        public int Id { get; set; }
        public Guid TicketId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? TicketPrice { get; set; }

        public DateTime Birthday { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
