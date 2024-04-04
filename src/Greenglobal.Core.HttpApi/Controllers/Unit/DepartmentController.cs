using Greenglobal.Core.Models;
using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Greenglobal.Core.Controllers
{
    [Route("api/system/departments")]
    public class DepartmentController : CoreController
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(PageBaseRequest pageRequest, SearchBaseRequest request)
        {
            var result = await _service.GetListDepartmentAsync(pageRequest, request);
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
        public async Task<IActionResult> GetMultiLevelAsync(Guid id)
        {
            var result = await _service.GetByIdMultiLevelAync(id);
            if (result.Data == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DepartmentRequest request)
        {
            var result = await _service.CreateDepartmentAsync(request);
            if (!result.Data)
                return BadRequest(result);
            return CreatedAtAction(null, result);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] DepartmentRequest request)
        {
            var result = await _service.UpdateDepartmentAsync(id, request);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteDepartmentAsync(id);
            if (!result.Data)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
