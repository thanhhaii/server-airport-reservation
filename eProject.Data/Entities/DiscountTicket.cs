using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public class DiscountTicket
    {
        public int Id { get; set; }
        public int? TicketPlaneInformationId { get; set; }
        public int PercentDiscount { get; set; }
        public DateTime Since { get; set; }
        public DateTime ToDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual TicketPlaneInformation TicketPlaneInformation { get; set; }
    }
}
