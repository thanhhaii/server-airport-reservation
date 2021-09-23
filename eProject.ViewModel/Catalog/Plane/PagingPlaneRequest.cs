using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eProject.ViewModel.Common;

namespace eProject.ViewModel.Catalog.Plane
{
    public class PagingPlaneRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
        public int? AirlineId { get; set; }
    }
}
