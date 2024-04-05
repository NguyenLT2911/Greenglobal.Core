using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
