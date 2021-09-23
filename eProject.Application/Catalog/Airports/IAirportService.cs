using System.Collections.Generic;
using System.Threading.Tasks;
using eProject.ViewModel.Catalog.Airport;
using eProject.ViewModel.Common;

namespace eProject.Application.Catalog.Airports
{
    public interface IAirportService
    {
        Task<ApiResult<AirportModel>> CreateAirport(AirportCreateRequest request);

        Task<ApiResult<bool>> UpdateAirport(int id, AirportCreateRequest request);

        Task<ApiResult<bool>> DeleteAirport(int id);

        Task<ApiResult<PagedResult<AirportResponse>>> GetPagingAirport(AirportPagingRequest request);

        Task<ApiResult<List<AirportResponse>>> GetAllAirport();
    }
}
