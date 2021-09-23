using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Application.Catalog.Tickets;
using eProject.ViewModel.Catalog.Ticket;
using Microsoft.AspNetCore.Authorization;

namespace eProject.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {

        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateRequest request)
        {
            var result = await _ticketService.CreateNewTicket(request);
            if (result.IsSuccessed)
            {
                return CreatedAtAction("CreateTicket", result.ResultObject);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetFlightByTicket(Guid id)
        {
            var result = await _ticketService.GetFlightByTicket(id);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest();
        }

        [HttpGet("myTicket/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetMyTicket(Guid id)
        {
            var result = await _ticketService.GetMyTicket(id);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest();
        }

        [HttpGet("myTicketAll/{idTicket}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetTicketAll(Guid idTicket)
        {
            var result = await _ticketService.GetTicketDetail(idTicket);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest();
        }
    }
}
