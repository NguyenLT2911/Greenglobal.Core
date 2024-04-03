using Greenglobal.Core.Models;
using Greenglobal.Core.Repositories;
using System;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    [RemoteService(IsMetadataEnabled = false)]
    public class ActionService :
        CrudAppService<
        Entities.Action,
        ActionResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        ActionRequest,
        ActionRequest>,
        IActionService
    {
        private readonly IActionRepository _repository;

        public ActionService(IActionRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
