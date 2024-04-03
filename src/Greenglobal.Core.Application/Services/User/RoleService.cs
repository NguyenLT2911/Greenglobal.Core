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
    public class RoleService :
        CrudAppService<
        Role,
        RoleResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        RoleRequest,
        RoleRequest>,
        IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
