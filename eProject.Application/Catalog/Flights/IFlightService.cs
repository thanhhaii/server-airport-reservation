using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eProject.ViewModel.Catalog.Flight;
using eProject.ViewModel.Common;

namespace eProject.Application.Catalog.Flights
{
    public interface IFlightService
    {
        Task<ApiResult<bool>> CreateFlight(CreateFlightRequest request);
        Task<ApiResult<PagedResult<FlightDetailResponse>>> FindFlight(FindFlightRequest request);
        Task<ApiResult<PagedResult<FlightDetailResponse>>> GetAllFlight(PagingRequestBase request);
        Task<ApiResult<FlightDetailModel>> GetFlight(Guid id);
    }
}
