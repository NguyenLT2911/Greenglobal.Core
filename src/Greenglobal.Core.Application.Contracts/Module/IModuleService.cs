using Greenglobal.Core.Models;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface IModuleService :
        ICrudAppService<
            ModuleResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            ModuleRequest,
            ModuleRequest>
    {
    }
}
