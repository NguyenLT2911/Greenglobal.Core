using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
