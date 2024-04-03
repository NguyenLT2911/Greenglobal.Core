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
    public class UserService :
        CrudAppService<
        User,
        UserResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        UserRequest,
        UserRequest>,
        IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
