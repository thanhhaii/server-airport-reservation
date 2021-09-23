using eProject.Data.EF;
using eProject.Data.Entities;
using eProject.ViewModel.Catalog.Airline;
using eProject.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eProject.Application.Catalog.Airlines
{
    public class AirlineService : IAirlineService
    {
        private readonly EProjectDBContext _dbContext;

        public AirlineService(EProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<AirlineModel>> Create(AirlineModel request)
        {
            var airline = await _dbContext.Airlines.SingleOrDefaultAsync(a => a.AirlineName == request.AirlineName);
            if (airline != null)
            {
                return new ApiErrorResult<AirlineModel>("Airline is exists");
            }
            var newAirline = new Airline()
            {
                AirlineName = request.AirlineName
            };
            var result = await _dbContext.AddAsync(newAirline);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<AirlineModel>(new AirlineModel()
            {
                AirlineId = result.Entity.AirlineId,
                AirlineName = result.Entity.AirlineName
            });
        }

        public async Task<ApiResult<bool>> Detele(int id)
        {
            var airline = await _dbContext.Airlines.FindAsync(id);
            if (airline == null)
            {
                return new ApiErrorResult<bool>("Airline doesn't exists");
            }
            _dbContext.Airlines.Remove(airline);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<AirlineModel>>> GetAll()
        {
            var data = await _dbContext.Airlines.Select(a => new AirlineModel()
            {
                AirlineId = a.AirlineId,
                AirlineName = a.AirlineName
            }).ToListAsync();
            var total = await _dbContext.Airlines.CountAsync();
            var pageResult = new PagedResult<AirlineModel>
            {
                Items = data,
                TotalRecord = total
            };
            return new ApiSuccessResult<PagedResult<AirlineModel>>(pageResult);
        }

        public async Task<ApiResult<bool>> Update(AirlineModel request)
        {
            var airline = await _dbContext.Airlines.FindAsync(request.AirlineId);
            if (airline == null)
            {
                return new ApiErrorResult<bool>("The city doesn't exist");
            }
            airline.AirlineName = request.AirlineName;
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}
