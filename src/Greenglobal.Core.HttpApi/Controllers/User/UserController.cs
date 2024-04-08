using Greenglobal.Core.Models;
using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Greenglobal.Core.Controllers
{
    [Route("api/system/users")]
    public class UserController : CoreController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(PageBaseRequest pageRequest, SearchUserRequest request)
        {
            var result = await _service.GetListUserAsync(pageRequest, request);
            if (result.Data == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _service.GetByIdAync(id);
            if (result.Data == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserRequest request)
        {
            var result = await _service.CreateUserAsync(request);
            if (!result.Data)
                return BadRequest(result);
            return CreatedAtAction(null, result);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UserRequest request)
        {
            var result = await _service.UpdateUserAsync(id, request);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteUserAsync(id);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
