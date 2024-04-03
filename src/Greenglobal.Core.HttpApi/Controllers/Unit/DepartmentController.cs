using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
