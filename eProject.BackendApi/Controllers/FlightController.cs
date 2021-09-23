using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Application.Catalog.Flights;
using eProject.ViewModel.Catalog.Flight;
using eProject.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;

namespace eProject.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateFlight(CreateFlightRequest request)
        {
            var result = await _flightService.CreateFlight(request);
            if (result.IsSuccessed) return CreatedAtAction("CreateFlight", result.ResultObject);
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<FlightDetailResponse>), 200)]
        public async Task<IActionResult> GetFindFlight([FromQuery] FindFlightRequest request)
        {
            var result = await _flightService.FindFlight(request);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest();
        }

        [HttpGet("get-flight-paging")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResult<FlightDetailResponse>), 200)]
        public async Task<IActionResult> GetFlightPaging([FromQuery]PagingRequestBase request)
        {
            var result = await _flightService.GetAllFlight(request);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FlightDetailModel), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> GetFlightDetail(Guid id)
        {
            var result = await _flightService.GetFlight(id);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest();
        }
    }
}
