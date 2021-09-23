using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public partial class FlightDetail
    {
        public Guid FlightDetailId { get; set; }
        public Guid FlightId { get; set; }
        public int AirportGoId { get; set; }
        public int DestinationAirportId { get; set; }
        public int RestFirstClassChair { get; set; }
        public int RestBusinessChair { get; set; }
        public int RestPremiumEconomyChair { get; set; }
        public int RestEconomyChair { get; set; }
        public double PriceNet { get; set; }

        public virtual Airport AirportGoNavigation { get; set; }
        public virtual Airport DestinationAirportNavigation { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
