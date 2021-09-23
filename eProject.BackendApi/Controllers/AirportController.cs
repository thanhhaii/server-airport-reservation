using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Application.Catalog.Airports;
using eProject.ViewModel.Catalog.Airport;
using eProject.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;

namespace eProject.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(401)]

    public class AirportController : Controller
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<AirportResponse>), 200)]
        public async Task<IActionResult> Airport()
        {
            var result = await _airportService.GetAllAirport();
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest();
        }

        //GetPaging Airport 
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResult<AirportResponse>), 200)]
        public async Task<IActionResult> Airport([FromQuery] AirportPagingRequest request)
        {
            var result = await _airportService.GetPagingAirport(request);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest();
        }


        //Create Airport
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AirportModel), 201)]
        public async Task<IActionResult> Airport([FromBody] AirportCreateRequest request)
        {
            var result = await _airportService.CreateAirport(request);
            if (result.IsSuccessed)
            {
                return CreatedAtAction("Airport", result.ResultObject);
            }

            return BadRequest();
        }

        //Update Airport
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateAirport(int id, [FromBody] AirportCreateRequest request)
        {
            var result = await _airportService.UpdateAirport(id, request);
            if (result.IsSuccessed)
            {
                return Ok();
            }

            return BadRequest();
        }

        //Delete Airport 
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteAirport(int id)
        {
            var result = await _airportService.DeleteAirport(id);
            if (result.IsSuccessed)
            {
                return Ok();
            }

            return BadRequest(result.Message);
        }
    }
}
