using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public partial class RefundPolicy
    {
        public int Id { get; set; }
        public int TimeBeforeRefundTicket { get; set; }
        public int RefundPercent { get; set; }
    }
}
