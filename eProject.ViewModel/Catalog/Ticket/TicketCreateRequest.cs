using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Ticket
{
    public class TicketCreateRequest
    {
        public Guid FlightId { get; set; }
        public List<CustomerInfo> CustomerInfos { get; set; }
        public Guid UserId { get; set; }
        public int TicketType { get; set; }
        public string Note { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public double TotalPrice { get; set; }
        public int TotalTicket { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }

    }
}
