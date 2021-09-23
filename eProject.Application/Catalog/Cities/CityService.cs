using eProject.Data.EF;
using eProject.Data.Entities;
using eProject.ViewModel.Catalog.City;
using eProject.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Application.Catalog.Cities
{
    public class CityService : ICityService
    {
        private readonly EProjectDBContext _dbContext;

        public CityService(EProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<CityResponse>> AddCity(CityRequest request)
        {
            var city = await _dbContext.Cities.SingleOrDefaultAsync(c => c.CityName == request.CityName);
            if (city != null)
            {
                return new ApiErrorResult<CityResponse>("CityName is exists");
            }
            city = new City()
            {
                CityName = request.CityName,
                CountryName = request.CountryName
            };
            await _dbContext.Cities.AddAsync(city);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<CityResponse>(new CityResponse()
            {
                Id = city.CityId,
                CityName = city.CityName,
                CountryName = city.CountryName
            });
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var city = await _dbContext.Cities.FindAsync(id);
            if (city == null)
            {
                return new ApiErrorResult<bool>("The city doesn't exist");
            }
            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> EditCity(int id, CityRequest request)
        {
            var city = await _dbContext.Cities.FindAsync(id);
            if (city == null)
            {
                return new ApiErrorResult<bool>("The city doesn't exist");
            }
            city.CityName = request.CityName;
            city.CountryName = request.CountryName;
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<CityResponse>>> GetCityPaging(GetPagingCityRequest request)
        {
            var query = _dbContext.Cities.Select(c => c);
            if (!string.IsNullOrEmpty(request.Keyword)) { 
                query = query.Where(c => c.CityName.Contains(request.Keyword) || c.CountryName.Contains(request.Keyword));
            }

            var totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CityResponse()
                {
                    Id = c.CityId,
                    CityName = c.CityName,
                    CountryName = c.CountryName
                }).ToListAsync();
            var pageResult = new PagedResult<CityResponse>
            {
                TotalRecord = totalRow,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<CityResponse>>(pageResult);
        }
    }
}
