using eProject.Data.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public class Plane
    {
        public Plane()
        {
            Flights = new HashSet<Flight>();
        }

        public int PlaneId { get; set; }
        public int AirlineId { get; set; }
        public string PlaneName { get; set; }
        public int TotalFirstClassChair { get; set; }
        public int TotalBusinessChair { get; set; }
        public int TotalPremiumEconomyChair { get; set; }
        public int TotalEconomyChair { get; set; }

        public virtual Airline Airline { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
