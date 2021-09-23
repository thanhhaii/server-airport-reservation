using eProject.Application.Catalog.Cities;
using eProject.ViewModel.Catalog.City;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eProject.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetCity([FromQuery]GetPagingCityRequest request)
        {
            var result = await _cityService.GetCityPaging(request);
            if (result.IsSuccessed)
            {
                return Ok(result.ResultObject);
            }
            return BadRequest(result.Message);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCity(CityRequest request)
        {
            var result = await _cityService.AddCity(request);
            if (result.IsSuccessed)
            {
                return CreatedAtAction("CreateCity", result.ResultObject);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity(int id, CityRequest request)
        {
            var result = await _cityService.EditCity(id, request);
            if (result.IsSuccessed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var result = await _cityService.Delete(id);
            if (result.IsSuccessed)
            {
                return Ok();
            }

            return BadRequest(result.Message);
        }

    }
}
