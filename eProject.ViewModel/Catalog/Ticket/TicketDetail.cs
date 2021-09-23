using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Ticket
{
    public class TicketDetail
    {
        public DateTime TimeFlight { get; set; }
        public string AirportGo { get; set; }
        public string ArrivalAirport { get; set; }
        public string AirlineName { get; set; }
        public DateTime ArrivalTime { get; set; }
        public Guid TicketId { get; set; }
        public string CityGo { get; set; }
        public string ArrivalCity { get; set; }
        public string PlaneName { get; set; }
    }
}
