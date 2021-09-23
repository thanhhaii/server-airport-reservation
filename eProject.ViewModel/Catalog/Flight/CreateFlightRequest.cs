using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Flight
{
    public class CreateFlightRequest
    {
        public int PlaneId { get; set; }
        public DateTime FlightTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AirportGo { get; set; }
        public int DestinationAirport { get; set; }
        public double PriceNet { get; set; }


    }
}
