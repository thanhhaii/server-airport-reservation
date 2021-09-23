using System.Collections.Generic;
using eProject.Data.Entities;
using eProject.ViewModel.Catalog.Plane;
using eProject.ViewModel.Common;
using System.Threading.Tasks;

namespace eProject.Application.Catalog.Planes
{
    public interface IPlaneService
    {
        Task<ApiResult<Plane>> CreatePlane(PlaneRequest request);

        Task<ApiResult<bool>> UpdatePlane(int id, PlaneRequest request);

        Task<ApiResult<bool>> DeletePlane(int id);

        Task<ApiResult<PagedResult<PlaneResult>>> GetPagingPlane(PagingPlaneRequest request);

        Task<ApiResult<List<PlaneResultSelected>>> GetAllPlane();

    }
}
