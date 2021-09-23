using eProject.Application.System.User;
using eProject.ViewModel.Common;
using eProject.ViewModel.System;
using eProject.ViewModel.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eProject.BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Authencate(request);
            if (string.IsNullOrEmpty(result.ResultObject))
            {
                return BadRequest(result.Message);
            }
            return Ok(new
            {
                token = result.ResultObject,
            });
        }

        [HttpGet("me")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserViewModel),200)]
        public async Task<IActionResult> CurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }
            var user = await _userService.GetById(Guid.Parse(userId));
            return Ok(user.ResultObject);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user.ResultObject);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var users = await _userService.GetUserPaging(request);
            return Ok(users);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResult), 201)]
        [ProducesResponseType(typeof(BaseResult), 400)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(new BaseResult
                {
                    Message = result.Message,
                    Status = result.IsSuccessed,
                });
            }
            return CreatedAtAction("Register", new BaseResult
            {
                Status = result.IsSuccessed,
                Message = result.Message
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.RoleAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.Delete(id);
            return Ok(result.ResultObject);
        }               
    }
}
