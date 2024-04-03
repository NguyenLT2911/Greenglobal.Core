using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greenglobal.Core.Controllers
{
    [Route("api/system/permissions")]
    public class PermissionController : CoreController
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }
    }
}
