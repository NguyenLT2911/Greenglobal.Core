using Greenglobal.Core.Models;
using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Greenglobal.Core.Controllers
{
    [Route("api/system/units")]
    public class UnitController : CoreController
    {
        private readonly IUnitService _service;

        public UnitController(IUnitService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(PageBaseRequest pageRequest, SearchBaseRequest request)
        {
            var result = await _service.GetListUnitAsync(pageRequest, request);
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

        [HttpGet, Route("{id}/multilevel")]
        public async Task<IActionResult> GetByIdMultiLevelAync(Guid id)
        {
            var result = await _service.GetByIdMultiLevelAync(id);
            if (result.Data == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet, Route("{id}/departments/multilevel")]
        public async Task<IActionResult> GetByIdMultiLevelHaveDepartmentAync(Guid id)
        {
            var result = await _service.GetByIdMultiLevelHaveDepartmentAync(id);
            if (result.Data == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]UnitRequest request)
        {
            var result = await _service.CreateUnitAsync(request);
            if (!result.Data)
                return BadRequest(result);
            return CreatedAtAction(null, result);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UnitRequest request)
        {
            var result = await _service.UpdateInitAsync(id, request);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteUnitAsync(id);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }
    }
}