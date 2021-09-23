using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eProject.ViewModel.Common;

namespace eProject.ViewModel.Catalog.Airport
{
    public class AirportPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }
    }
}
