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
    public class PermissionService :
        CrudAppService<
        Permission,
        PermissionResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        PermissionRequest,
        PermissionRequest>,
        IPermissionService
    {
        private readonly IPermissionRepository _repository;

        public PermissionService(IPermissionRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
