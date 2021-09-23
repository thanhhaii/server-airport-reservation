using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace eProject.Data.Entities
{
    public partial class Airport
    {
        public Airport()
        {
            FlightDetailsGo = new HashSet<FlightDetail>();
            FlightDetailDestination = new HashSet<FlightDetail>();
        }
        
        public int AirportId { get; set; }
        public string AirportName { get; set; }
        public int CityId { get; set; }

        public virtual ICollection<FlightDetail> FlightDetailsGo { get; set; }
        public virtual ICollection<FlightDetail> FlightDetailDestination { get; set; }
        public virtual City City { get; set; }
    }
}
