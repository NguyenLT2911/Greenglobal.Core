using Greenglobal.Core.Entities;
using Greenglobal.Core.Models;
using Greenglobal.Core.Repositories;
using System;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    [RemoteService(IsMetadataEnabled = false)]
    public class ModuleService :
        CrudAppService<
        Module,
        ModuleResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        ModuleRequest,
        ModuleRequest>,
        IModuleService
    {
        private readonly IModuleRepository _repository;

        public ModuleService(IModuleRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
