using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Flight
{
    public class FindFlightResponse
    {
        public string Airline { get; set; }
        public string FlyingFrom { get; set; }
        public DateTime FlightTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double Price { get; set; }
        public double PriceNet { get; set; }
        public double TicketPrice { get; set; }
    }
}
