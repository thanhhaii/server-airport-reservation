using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Flight
{
    public class FlightDetailModel : FlightDetailResponse
    {
        public double PriceTicketFirstClass { get; set; }
        public double PriceTicketBusinessClass { get; set; }
        public double PricePremiumEconomyClass { get; set; }
        public double PriceEconomyClass { get; set; }
        public int RestTicketFirstClass { get; set; }
        public int RestTicketBusinessClass { get; set; }
        public int RestTicketPremiumEconomyClass { get; set; }
        public int RestTicketEconomyClass { get; set; }

    }
}
