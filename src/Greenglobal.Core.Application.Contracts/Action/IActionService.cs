using Greenglobal.Core.Models;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface IActionService :
        ICrudAppService<
            ActionResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            ActionRequest,
            ActionRequest>
    {
    }
}
