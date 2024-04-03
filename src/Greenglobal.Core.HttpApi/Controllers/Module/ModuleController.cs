using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greenglobal.Core.Controllers
{
    [Route("api/system/modules")]
    public class ModuleController : CoreController
    {
        private readonly IModuleService _service;

        public ModuleController(IModuleService service)
        {
            _service = service;
        }
    }
}
