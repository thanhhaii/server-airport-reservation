using eProject.Data.Entities;
using System;
using System.Collections.Generic;

#nullable disable

namespace eProject.Data.Entities
{
    public class Airline
    {
        public Airline()
        {
            Planes = new HashSet<Plane>();
        }

        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public int Vat { get; set; }
        public double ScreeningSecurityFee { get; set; }
        public double AirportFee { get; set; }
        public double AdministrationFee { get; set; }
        public double Surcharge { get; set; }
        public double PriceNetFristClass { get; set; }
        public double PriceNetBusinessClass { get; set; }
        public double PriceNetPremiumEconomicClass { get; set; }

        public virtual ICollection<Plane> Planes { get; set; }
    }
}
