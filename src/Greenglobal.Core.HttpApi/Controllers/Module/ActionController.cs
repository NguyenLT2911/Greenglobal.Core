using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greenglobal.Core.Controllers
{
    [Route("api/system/actions")]
    public class ActionController : CoreController
    {
        private readonly IActionService _service;

        public ActionController(IActionService service)
        {
            _service = service;
        }
    }
}
