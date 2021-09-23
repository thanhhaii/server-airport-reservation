using eProject.ViewModel.Catalog.Airline;
using eProject.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.Application.Catalog.Airlines
{
    public interface IAirlineService
    {
        Task<ApiResult<AirlineModel>> Create(AirlineModel request);
        Task<ApiResult<bool>> Update(AirlineModel request);
        Task<ApiResult<bool>> Detele(int id);
        Task<ApiResult<PagedResult<AirlineModel>>> GetAll();
    }
}
