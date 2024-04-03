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
    public class DepartmentService :
        CrudAppService<
        Department,
        DepartmentResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        DepartmentRequest,
        DepartmentRequest>,
        IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
