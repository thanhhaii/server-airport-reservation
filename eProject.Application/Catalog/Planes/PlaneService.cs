using eProject.Data.EF;
using eProject.Data.Entities;
using eProject.ViewModel.Catalog.Plane;
using eProject.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eProject.Application.Catalog.Planes
{
    public class PlaneService : IPlaneService
    {
        private readonly EProjectDBContext _dbContext;

        public PlaneService(EProjectDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<ApiResult<Plane>> CreatePlane(PlaneRequest request)
        {
            var plane = new Plane()
            {
                AirlineId = request.AirlineId,
                PlaneName = request.PlaneName,
                TotalFirstClassChair = request.TotalFirstClassChair,
                TotalBusinessChair = request.TotalBusinessChair,
                TotalPremiumEconomyChair = request.TotalPremiumEconomyChair,
                TotalEconomyChair = request.TotalEconomyChair
            };

            await _dbContext.Planes.AddAsync(plane);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<Plane>(plane);
        }

        public async Task<ApiResult<bool>> DeletePlane(int id)
        {
            var plane = await _dbContext.Planes.FindAsync(id);
            if(plane == null)
            {
                return new ApiErrorResult<bool>("No plane found");
            }
            _dbContext.Planes.Remove(plane);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<PlaneResult>>> GetPagingPlane(PagingPlaneRequest request)
        {
            var query = _dbContext.Planes.Select(c => c);
            if (request.AirlineId != null)
            {
                query = query.Where(p => p.AirlineId == request.AirlineId);
            }
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(p => p.PlaneName.Contains(request.Keyword));
            }

            var totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new PlaneResult()
                {
                    PlaneId = p.PlaneId,
                    AirlineName = p.Airline.AirlineName,
                    PlaneName = p.PlaneName,
                    TotalFirstClassChair = p.TotalFirstClassChair,
                    TotalBusinessChair = p.TotalBusinessChair,
                    TotalEconomyChair = p.TotalEconomyChair,
                    TotalPremiumEconomyChair = p.TotalPremiumEconomyChair
                }).ToListAsync();
            var pageResult = new PagedResult<PlaneResult>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<PlaneResult>>(pageResult);
        }

        public async Task<ApiResult<List<PlaneResultSelected>>> GetAllPlane()
        {
            var planes = await _dbContext.Planes.Select(p => new PlaneResultSelected()
            {
                PlaneId = p.PlaneId,
                PlaneName = p.PlaneName,
                AirlineName = p.Airline.AirlineName
            }).ToListAsync();
            return new ApiSuccessResult<List<PlaneResultSelected>>(planes);
        }

        public async Task<ApiResult<bool>> UpdatePlane(int id, PlaneRequest request)
        {
            var plane = await _dbContext.Planes.FindAsync(id);
            if(plane == null)
            {
                return new ApiErrorResult<bool>("No plane found");
            }
            plane.PlaneName = request.PlaneName;
            plane.TotalBusinessChair = request.TotalBusinessChair;
            plane.TotalFirstClassChair = request.TotalFirstClassChair;
            plane.TotalPremiumEconomyChair = request.TotalPremiumEconomyChair;
            plane.TotalEconomyChair = request.TotalEconomyChair;
            _dbContext.Planes.Update(plane);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}
