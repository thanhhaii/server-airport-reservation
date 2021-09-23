using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eProject.ViewModel.Catalog.Ticket;
using eProject.ViewModel.Common;

namespace eProject.Application.Catalog.Tickets
{
    public interface ITicketService
    {
        Task<ApiResult<Guid>> CreateNewTicket(TicketCreateRequest request);
        Task<ApiResult<Object>> GetFlightByTicket(Guid id);
        Task<ApiResult<List<TicketDetail>>> GetMyTicket(Guid id);
        Task<ApiResult<TicketDetailAll>> GetTicketDetail(Guid id);
    }
}
