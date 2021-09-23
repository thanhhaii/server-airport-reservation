using eProject.Application.Catalog.Airlines;
using eProject.ViewModel.Catalog.Airline;
using eProject.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eProject.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineService _airlineService;

        public AirlineController(IAirlineService airlineService)
        {
            _airlineService = airlineService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResult<AirlineModel>), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<IActionResult> Get()
        {
            var result = await _airlineService.GetAll();
            return Ok(result.ResultObject);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AirlineModel), 201)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<IActionResult> CreateAirline([FromBody] AirlineModel request)
        {
            var result = await _airlineService.Create(request);
            if (result.IsSuccessed)
            {
                return CreatedAtAction("CreateAirline", result.ResultObject);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AirlineModel value)
        {
            var result = await _airlineService.Update(value);
            if (result.IsSuccessed)
            {
                return Ok();
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _airlineService.Detele(id);
            if (result.IsSuccessed)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
