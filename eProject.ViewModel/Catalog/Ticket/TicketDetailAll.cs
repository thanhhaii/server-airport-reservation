using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Ticket
{
    public class TicketDetailAll : TicketDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public List<CustomerInfo> People { get; set; }
    }
}
