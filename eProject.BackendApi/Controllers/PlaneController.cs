using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Application.Catalog.Planes;
using eProject.ViewModel.Catalog.Plane;
using Microsoft.AspNetCore.Authorization;

namespace eProject.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class PlaneController : ControllerBase
    {
        private readonly IPlaneService _planeService;

        public PlaneController(IPlaneService planeService)
        {
            _planeService = planeService;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<PlaneResultSelected>),200)]
        public async Task<IActionResult> GetAllPlane()
        {
            var result = await _planeService.GetAllPlane();
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetPlane([FromQuery]PagingPlaneRequest request)
        {
            var result = await _planeService.GetPagingPlane(request);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }

            return BadRequest(result.Message);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreatePlane([FromBody]PlaneRequest request)
        {
            var result = await _planeService.CreatePlane(request);
            if (result.IsSuccessed)
            {
                return CreatedAtAction("CreatePlane", result.ResultObject);
            }

            return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdatePlane(int id,[FromBody]PlaneRequest request)
        {
            var result = await _planeService.UpdatePlane(id, request);
            if (result.IsSuccessed)
            {
                return Ok();
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlane(int id)
        {
            var result = await _planeService.DeletePlane(id);
            if (result.IsSuccessed)
            {
                return Ok();
            }

            return BadRequest(result.Message);
        }

    }
}
