using Greenglobal.Core.Models;
using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Greenglobal.Core.Controllers
{
    [Route("api/system/functions")]
    public class FunctionController : CoreController
    {
        private readonly IFunctionService _service;

        public FunctionController(IFunctionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(PageBaseRequest pageRequest, SearchBaseRequest request)
        {
            var result = await _service.GetListFunctionAsync(pageRequest, request);
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

        [HttpGet, Route("{id}/multilevel")]
        public async Task<IActionResult> GetHavePermissionAsync(Guid id)
        {
            var result = await _service.GetHavePermissionByIdAsync(id);
            if (result.Data == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] FunctionRequest request)
        {
            var result = await _service.CreateFunctionAsync(request);
            if (!result.Data)
                return BadRequest(result);
            return CreatedAtAction(null, result);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] FunctionRequest request)
        {
            var result = await _service.UpdateFunctionAsync(id, request);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteFunctionAsync(id);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
