using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eProject.Data.EF;
using eProject.Data.Entities;
using eProject.ViewModel.Catalog.Ticket;
using eProject.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using TicketDetail = eProject.ViewModel.Catalog.Ticket.TicketDetail;

namespace eProject.Application.Catalog.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly EProjectDBContext _dbContext;

        public TicketService(EProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ApiResult<Guid>> CreateNewTicket(TicketCreateRequest request)
        {
            var ticket = new Ticket()
            {
                FlightId = request.FlightId,
                TicketType = request.TicketType,
                UserId = request.UserId,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                TotalPrice = request.TotalPrice,
                TicketStatus = 1,
                PaymentMethod = request.PaymentMethod,
                Note = request.Note,
                BookingDate = DateTime.Now
            };
            var ticketResult = await _dbContext.Tickets.AddAsync(ticket);
            var ticketDetails = new List<Data.Entities.TicketDetail>();
            request.CustomerInfos.ForEach(c =>
            {
                var ticketDetail = new Data.Entities.TicketDetail()
                {
                    Birthday = c.Birthday,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    TicketPrice = c.TicketPrice,
                    TicketId = ticketResult.Entity.TicketId
                };
                ticketDetails.Add(ticketDetail);
            });
            var flightDetail = await _dbContext.FlightDetails.SingleOrDefaultAsync(f => f.FlightId == request.FlightId);
            switch (request.TicketType)
            {
                case 1:
                    flightDetail.RestFirstClassChair -= request.TotalTicket;
                    break;
                case 2:
                    flightDetail.RestBusinessChair -= request.TotalTicket;
                    break;
                case 3:
                    flightDetail.RestPremiumEconomyChair -= request.TotalTicket;
                    break;
                case 4:
                    flightDetail.RestEconomyChair -= request.TotalTicket;
                    break;
            };
            _dbContext.FlightDetails.Update(flightDetail);
            await _dbContext.TicketDetails.AddRangeAsync(ticketDetails);
            var resp = await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<Guid>(ticketResult.Entity.TicketId);
        }

        public async Task<ApiResult<object>> GetFlightByTicket(Guid id)
        {
            var ticket = await _dbContext.Tickets.FindAsync(id);
            var result = new
            {
                airportGo = ticket.Flight.FlightDetails.AirportGoNavigation.AirportName,
                arrivalAirport = ticket.Flight.FlightDetails.DestinationAirportNavigation.AirportName,
                timeFlight = ticket.Flight.FlightTime,
                arrivalTime = ticket.Flight.ArrivalTime,
                flightId = ticket.FlightId,
                airlineName = ticket.Flight.Plane.Airline.AirlineName,
                planeName = ticket.Flight.Plane.PlaneName
            };
            return new ApiSuccessResult<object>(result);
        }

        public async Task<ApiResult<List<TicketDetail>>> GetMyTicket(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            var response = user.Tickets.Select(t => new TicketDetail()
            {
                AirportGo = t.Flight.FlightDetails.AirportGoNavigation.AirportName,
                AirlineName = t.Flight.Plane.Airline.AirlineName,
                ArrivalTime = t.Flight.ArrivalTime,
                CityGo = t.Flight.FlightDetails.AirportGoNavigation.City.CityName,
                TicketId = t.TicketId,
                ArrivalAirport = t.Flight.FlightDetails.DestinationAirportNavigation.AirportName,
                ArrivalCity = t.Flight.FlightDetails.AirportGoNavigation.City.CityName,
                TimeFlight = t.Flight.FlightTime,
                PlaneName = t.Flight.Plane.PlaneName
            }).ToList();
            return new ApiSuccessResult<List<TicketDetail>>(response);
        }

        public async Task<ApiResult<TicketDetailAll>> GetTicketDetail(Guid id)
        {
            var ticket = await _dbContext.Tickets.FindAsync(id);
            var customerList = ticket.TicketDetails.Select(t => new CustomerInfo()
            {
                Birthday = t.Birthday,
                FirstName = t.FirstName,
                LastName = t.LastName,
            }).ToList();
            var response = new TicketDetailAll()
            {
                AirportGo = ticket.Flight.FlightDetails.AirportGoNavigation.AirportName,
                AirlineName = ticket.Flight.Plane.Airline.AirlineName,
                ArrivalTime = ticket.Flight.ArrivalTime,
                CityGo = ticket.Flight.FlightDetails.AirportGoNavigation.City.CityName,
                TicketId = ticket.TicketId,
                ArrivalAirport = ticket.Flight.FlightDetails.DestinationAirportNavigation.AirportName,
                ArrivalCity = ticket.Flight.FlightDetails.AirportGoNavigation.City.CityName,
                TimeFlight = ticket.Flight.FlightTime,
                PlaneName = ticket.Flight.Plane.PlaneName,
                BookingDate = ticket.BookingDate,
                Email = ticket.Email,
                FirstName = ticket.FirstName,
                LastName = ticket.LastName,
                PhoneNumber = ticket.PhoneNumber,
                TotalPrice = ticket.TotalPrice,
                People = customerList
            };
            return new ApiSuccessResult<TicketDetailAll>(response);
        }
    }
}
