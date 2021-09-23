using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public class City
    {
        public City()
        {
            Airports = new HashSet<Airport>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<Airport> Airports { get; set; }
    }
}
