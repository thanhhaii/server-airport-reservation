using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using eProject.Application.Catalog.Tickets;
using eProject.Data.EF;
using eProject.Data.Entities;
using eProject.ViewModel.Catalog.Flight;
using eProject.ViewModel.Catalog.Ticket;
using eProject.ViewModel.Common;
using Microsoft.EntityFrameworkCore;

namespace eProject.Application.Catalog.Flights
{
    public class FlightService : IFlightService
    {
        private readonly EProjectDBContext _dbContext;

        public FlightService(EProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<bool>> CreateFlight(CreateFlightRequest request)
        {
            var plane = await _dbContext.Planes.FindAsync(request.PlaneId);
            if (plane == null) return new ApiErrorResult<bool>("Plane doesn't exists");
            var flight = new Flight()
            {
                PlaneId = request.PlaneId,
                FlightTime = request.FlightTime,
                ArrivalTime = request.ArrivalTime,
                TotalSeats = (plane.TotalBusinessChair + plane.TotalEconomyChair + plane.TotalFirstClassChair +
                              plane.TotalPremiumEconomyChair)
            };
            var result = await _dbContext.Flights.AddAsync(flight);
            var flightDetail = new FlightDetail()
            {
                FlightId = result.Entity.FlightId,
                AirportGoId = request.AirportGo,
                DestinationAirportId = request.DestinationAirport,
                RestBusinessChair = plane.TotalBusinessChair,
                RestEconomyChair = plane.TotalEconomyChair,
                RestFirstClassChair = plane.TotalFirstClassChair,
                RestPremiumEconomyChair = plane.TotalPremiumEconomyChair,
                PriceNet = request.PriceNet,
                AirportGoNavigation = await _dbContext.Airports.FindAsync(request.AirportGo),
                DestinationAirportNavigation = await _dbContext.Airports.FindAsync(request.DestinationAirport)
            };
            await _dbContext.FlightDetails.AddAsync(flightDetail);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<FlightDetailResponse>>> FindFlight(FindFlightRequest request)
        {
            var query = _dbContext.Flights
                .Where(f => f.FlightDetails.AirportGoId == request.FlyingFrom && f.FlightDetails.DestinationAirportId == request.FlyingTo && f.FlightTime >= request.Departing).Select(f => f).Select(f => f);
            if (request.Returning != null)
            {
                query = query.Where(f => f.FlightTime <= request.Returning).Select(f => f);
            }

            var sumTicket = request.Adults + request.Children;
            query = request.TicketType switch
            {
                1 => query.Where(f => (f.FlightDetails.RestFirstClassChair - sumTicket) >= 0),
                2 => query.Where(f => (f.FlightDetails.RestBusinessChair - sumTicket) >= 0),
                3 => query.Where(f => (f.FlightDetails.RestPremiumEconomyChair - sumTicket) >= 0),
                4 => query.Where(f => (f.FlightDetails.RestEconomyChair - sumTicket) >= 0),
                _ => query
            };
            var totalRecord = await query.CountAsync();
            var data = request.TicketType switch
            {
                1 => await query.Select(f => new FlightDetailResponse()
                {
                    FlightId = f.FlightId,
                    PlaneName = f.Plane.PlaneName,
                    FlightTime = f.FlightTime,
                    ArrivalTime = f.ArrivalTime,
                    AirportGo = f.FlightDetails.AirportGoNavigation.AirportName,
                    CityGo = f.FlightDetails.AirportGoNavigation.City.CityName,
                    DestinationAirport = f.FlightDetails.DestinationAirportNavigation.AirportName,
                    DestinationCity = f.FlightDetails.DestinationAirportNavigation.City.CityName,
                    PriceTicket = (f.FlightDetails.PriceNet * f.Plane.Airline.PriceNetFristClass) +
                                  ((f.FlightDetails.PriceNet * f.Plane.Airline.PriceNetFristClass) *
                                   (f.Plane.Airline.Vat / 100)) + f.Plane.Airline.AdministrationFee +
                                  f.Plane.Airline.AirportFee + f.Plane.Airline.Surcharge +
                                  f.Plane.Airline.ScreeningSecurityFee,
                    AirlineName = f.Plane.Airline.AirlineName
                }).ToListAsync(),
                2 => await query.Select(f => new FlightDetailResponse()
                {
                    FlightId = f.FlightId,
                    PlaneName = f.Plane.PlaneName,
                    FlightTime = f.FlightTime,
                    ArrivalTime = f.ArrivalTime,
                    AirportGo = f.FlightDetails.AirportGoNavigation.AirportName,
                    CityGo = f.FlightDetails.AirportGoNavigation.City.CityName,
                    DestinationAirport = f.FlightDetails.DestinationAirportNavigation.AirportName,
                    DestinationCity = f.FlightDetails.DestinationAirportNavigation.City.CityName,
                    PriceTicket = (f.FlightDetails.PriceNet * f.Plane.Airline.PriceNetBusinessClass) +
                                  ((f.FlightDetails.PriceNet * f.Plane.Airline.PriceNetBusinessClass) *
                                   (f.Plane.Airline.Vat / 100)) + f.Plane.Airline.AdministrationFee +
                                  f.Plane.Airline.AirportFee + f.Plane.Airline.Surcharge +
                                  f.Plane.Airline.ScreeningSecurityFee,
                    AirlineName = f.Plane.Airline.AirlineName
                }).ToListAsync(),
                3 => await query.Select(f => new FlightDetailResponse()
                {
                    FlightId = f.FlightId,
                    PlaneName = f.Plane.PlaneName,
                    FlightTime = f.FlightTime,
                    ArrivalTime = f.ArrivalTime,
                    AirportGo = f.FlightDetails.AirportGoNavigation.AirportName,
                    CityGo = f.FlightDetails.AirportGoNavigation.City.CityName,
                    DestinationAirport = f.FlightDetails.DestinationAirportNavigation.AirportName,
                    DestinationCity = f.FlightDetails.DestinationAirportNavigation.City.CityName,
                    PriceTicket = (f.FlightDetails.PriceNet * f.Plane.Airline.PriceNetPremiumEconomicClass) +
                                  ((f.FlightDetails.PriceNet * f.Plane.Airline.PriceNetPremiumEconomicClass) *
                                   (f.Plane.Airline.Vat / 100)) + f.Plane.Airline.AdministrationFee +
                                  f.Plane.Airline.AirportFee + f.Plane.Airline.Surcharge +
                                  f.Plane.Airline.ScreeningSecurityFee,
                    AirlineName = f.Plane.Airline.AirlineName
                }).ToListAsync(),
                4 => await query.Select(f => new FlightDetailResponse()
                {
                    FlightId = f.FlightId,
                    PlaneName = f.Plane.PlaneName,
                    FlightTime = f.FlightTime,
                    ArrivalTime = f.ArrivalTime,
                    AirportGo = f.FlightDetails.AirportGoNavigation.AirportName,
                    CityGo = f.FlightDetails.AirportGoNavigation.City.CityName,
                    DestinationAirport = f.FlightDetails.DestinationAirportNavigation.AirportName,
                    DestinationCity = f.FlightDetails.DestinationAirportNavigation.City.CityName,
                    PriceTicket = f.FlightDetails.PriceNet +
                                  (f.FlightDetails.PriceNet * (f.Plane.Airline.Vat / 100)) + f.Plane.Airline.AdministrationFee +
                                  f.Plane.Airline.AirportFee + f.Plane.Airline.Surcharge +
                                  f.Plane.Airline.ScreeningSecurityFee,
                    AirlineName = f.Plane.Airline.AirlineName
                }).ToListAsync(),
                _ => throw new ArgumentOutOfRangeException()
            };
            return new ApiSuccessResult<PagedResult<FlightDetailResponse>>(new PagedResult<FlightDetailResponse>()
            {
                TotalRecord = totalRecord,
                Items = data
            });

        }

        public async Task<ApiResult<PagedResult<FlightDetailResponse>>> GetAllFlight(PagingRequestBase request)
        {
            var data = await _dbContext.Flights.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(f => new FlightDetailResponse()
            {
                FlightId = f.FlightId,
                PlaneName = f.Plane.PlaneName,
                FlightTime = f.FlightTime,
                ArrivalTime = f.ArrivalTime,
                AirportGo = f.FlightDetails.AirportGoNavigation.AirportName,
                CityGo = f.FlightDetails.AirportGoNavigation.City.CityName,
                DestinationAirport = f.FlightDetails.DestinationAirportNavigation.AirportName,
                DestinationCity = f.FlightDetails.DestinationAirportNavigation.City.CityName,
                AirlineName = f.Plane.Airline.AirlineName,
                PriceNet = f.FlightDetails.PriceNet
            }).OrderByDescending(f => f.FlightTime).ToListAsync();
            var totalRow = data.Count();
            var result = new PagedResult<FlightDetailResponse>()
            {
                Items = data,
                TotalRecord = totalRow
            };
            return new ApiSuccessResult<PagedResult<FlightDetailResponse>>(result);
        }

        public async Task<ApiResult<FlightDetailModel>> GetFlight(Guid id)
        {
            var flight = await _dbContext.Flights.FindAsync(id);
            if (flight == null)
            {
                return new ApiErrorResult<FlightDetailModel>("Flight doesn't exists");
            }

            var result = new FlightDetailModel()
            {
                FlightId = flight.FlightId,
                PlaneName = flight.Plane.PlaneName,
                AirlineName = flight.Plane.Airline.AirlineName,
                DestinationAirport = flight.FlightDetails.DestinationAirportNavigation.AirportName,
                DestinationCity = flight.FlightDetails.DestinationAirportNavigation.City.CityName,
                CityGo = flight.FlightDetails.AirportGoNavigation.City.CityName,
                AirportGo = flight.FlightDetails.AirportGoNavigation.AirportName,
                FlightTime = flight.FlightTime,
                ArrivalTime = flight.ArrivalTime,
                PriceNet = flight.FlightDetails.PriceNet,
                PriceTicketFirstClass = PriceTicket(flight.Plane.Airline, flight.FlightDetails.PriceNet, 1),
                PriceTicketBusinessClass = PriceTicket(flight.Plane.Airline, flight.FlightDetails.PriceNet, 2),
                PricePremiumEconomyClass = PriceTicket(flight.Plane.Airline, flight.FlightDetails.PriceNet, 3),
                PriceEconomyClass = PriceTicket(flight.Plane.Airline, flight.FlightDetails.PriceNet, 4),
                RestTicketFirstClass = flight.FlightDetails.RestFirstClassChair,
                RestTicketBusinessClass = flight.FlightDetails.RestBusinessChair,
                RestTicketPremiumEconomyClass = flight.FlightDetails.RestPremiumEconomyChair,
                RestTicketEconomyClass = flight.FlightDetails.RestEconomyChair,
            };
            return new ApiSuccessResult<FlightDetailModel>(result);
        }

        public double PriceTicket(Airline airline, double priceNet, int ticketType)
        {
            var priceNetPercent = ticketType switch
            {
                1 => airline.PriceNetFristClass,
                2 => airline.PriceNetBusinessClass,
                3 => airline.PriceNetPremiumEconomicClass,
                4 => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(ticketType), ticketType, null)
            };
            var price = (priceNet * priceNetPercent) + ((airline.Vat / 100) * (priceNet * priceNetPercent)) + airline.ScreeningSecurityFee + airline.AirportFee +
                        airline.Surcharge + airline.AdministrationFee;
            return price;
        }
    }
}
