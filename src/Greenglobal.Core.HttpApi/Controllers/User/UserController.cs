using Greenglobal.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greenglobal.Core.Controllers
{
    [Route("api/system/users")]
    public class UserController : CoreController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
    }
}
