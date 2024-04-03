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
    public class UnitService :
        CrudAppService<
        Unit,
        UnitResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        UnitRequest,
        UnitRequest>,
        IUnitService
    {
        private readonly IUnitRepository _repository;

        public UnitService(IUnitRepository repository) : base(repository)
        {
            _repository = repository;
        }

        
    }
}
