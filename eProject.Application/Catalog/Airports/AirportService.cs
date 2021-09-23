using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eProject.Data.EF;
using eProject.Data.Entities;
using eProject.ViewModel.Catalog.Airport;
using eProject.ViewModel.Common;
using Microsoft.EntityFrameworkCore;

namespace eProject.Application.Catalog.Airports
{
    public class AirportService : IAirportService
    {
        private readonly EProjectDBContext _dbContext;

        public AirportService(EProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<AirportModel>> CreateAirport(AirportCreateRequest request)
        {
            var airport = new Airport()
            {
                AirportName = request.AirportName,
                CityId = request.CityId
            };
            var checkExists = await _dbContext.Airports.SingleOrDefaultAsync(a =>
                a.AirportName == airport.AirportName && a.CityId == airport.CityId);
            if (checkExists != null)
            {
                return new ApiErrorResult<AirportModel>("Airport is exists");
            }

            await _dbContext.Airports.AddAsync(airport);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<AirportModel>();
        }

        public async Task<ApiResult<bool>> UpdateAirport(int id, AirportCreateRequest request)
        {
            var airport = await _dbContext.Airports.FindAsync(id);
            if (airport == null)
            {
                return new ApiErrorResult<bool>("Airport doesn't exists");
            }

            airport.AirportName = request.AirportName;
            airport.CityId = request.CityId;
            _dbContext.Airports.Update(airport);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteAirport(int id)
        {
            var airport = await _dbContext.Airports.FindAsync(id);
            if (airport == null)
            {
                return new ApiErrorResult<bool>("Airport doesn't exists");
            }

            _dbContext.Airports.Remove(airport);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<AirportResponse>>> GetPagingAirport(AirportPagingRequest request)
        {
            var query = _dbContext.Airports.Join(_dbContext.Cities, a => a.CityId, c => c.CityId, (a, c) => new AirportResponse
            {
                AirportId = a.AirportId,
                AirportName = a.AirportName,
                CityName = c.CityName,
                CountryName = c.CountryName,
                CityId = c.CityId
            }).Select(c => c);
            if (!string.IsNullOrEmpty(request.keyword))
            {
                query = query.Where(a => a.AirportName.Contains(request.keyword) || a.CityName.Contains(request.keyword));
            }

            var totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            return new ApiSuccessResult<PagedResult<AirportResponse>>(new PagedResult<AirportResponse>()
            {
                TotalRecord = totalRow,
                Items = data
            });
        }

        public async Task<ApiResult<List<AirportResponse>>> GetAllAirport()
        {
            var query = await _dbContext.Airports.Join(_dbContext.Cities, a => a.CityId, c => c.CityId, (a, c) => new AirportResponse
            {
                AirportId = a.AirportId,
                AirportName = a.AirportName,
                CityName = c.CityName,
                CountryName = c.CountryName,
                CityId = c.CityId
            }).Select(c => c).ToListAsync();

            return new ApiSuccessResult<List<AirportResponse>>(query);
        }
    }
}
