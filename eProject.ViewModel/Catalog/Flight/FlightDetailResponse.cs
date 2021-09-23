using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Flight
{
    public class FlightDetailResponse
    {
        public Guid FlightId { get; set; }
        public string PlaneName { get; set; }
        public DateTime FlightTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string AirportGo { get; set; }
        public string CityGo { get; set; }
        public string DestinationAirport { get; set; }
        public string DestinationCity { get; set; }
        public double PriceTicket { get; set; }
        public double PriceNet { get; set; }
        public string AirlineName { get; set; }
    }
}
