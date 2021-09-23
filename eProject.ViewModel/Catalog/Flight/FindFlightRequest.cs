using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Flight
{
    public class FindFlightRequest
    {
        public int FlyingFrom { get; set; }
        public int? FlyingTo { get; set; }
        public DateTime Departing { get; set; }
        public DateTime? Returning { get; set; }
        public int? Adults { get; set; }
        public int? Children { get; set; }
        public int? Baby { get; set; }
        public int TicketType { get; set; }
    }
}
