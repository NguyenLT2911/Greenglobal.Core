using Greenglobal.Core.Models;
using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Greenglobal.Core.Controllers
{
    [Route("api/system/roles")]
    public class RoleController : CoreController
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(PageBaseRequest pageRequest, SearchBaseRequest request)
        {
            var result = await _service.GetListRoleAsync(pageRequest, request);
            if (result.Data == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.Data == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RoleRequest request)
        {
            var result = await _service.CreateRoleAsync(request);
            if (!result.Data)
                return BadRequest(result);
            return CreatedAtAction(null, result);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] RoleRequest request)
        {
            var result = await _service.UpdateRoleAsync(id, request);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteRoleAsync(id);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
