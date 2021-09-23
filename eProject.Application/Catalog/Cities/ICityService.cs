using eProject.ViewModel.Catalog.City;
using eProject.ViewModel.Common;
using eProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.Application.Catalog.Cities
{
    public interface ICityService
    {
        Task<ApiResult<CityResponse>> AddCity(CityRequest request);
        Task<ApiResult<bool>> EditCity(int id,CityRequest request);
        Task<ApiResult<bool>> Delete(int id);
        Task<ApiResult<PagedResult<CityResponse>>> GetCityPaging(GetPagingCityRequest request);
    }
}
