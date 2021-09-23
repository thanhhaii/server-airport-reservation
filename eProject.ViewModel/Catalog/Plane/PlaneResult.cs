using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.Plane
{
    public class PlaneResult
    {
        public int PlaneId { get; set; }
        public string AirlineName { get; set; }

        public string PlaneName { get; set; }

        public int TotalFirstClassChair { get; set; }

        public int TotalBusinessChair { get; set; }

        public int TotalPremiumEconomyChair { get; set; }

        public int TotalEconomyChair { get; set; }

    }
}
