using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Airport
{
    public class AirportResponse
    {
        public int AirportId { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }

    }
}
