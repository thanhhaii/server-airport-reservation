using eProject.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.ViewModel.Catalog.City
{
    public class GetPagingCityRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
