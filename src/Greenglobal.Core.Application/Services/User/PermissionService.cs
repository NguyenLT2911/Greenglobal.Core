using Greenglobal.Core.Constants;
using Greenglobal.Core.Entities;
using Greenglobal.Core.Models;
using Greenglobal.Core.Repositories;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

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

        public async Task<BaseResponse<bool>> CreatePermissionAsync(PermissionRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                var data = await AsyncExecuter.FirstOrDefaultAsync
                    (_repository.GetByRoleFunction(request.RoleId, request.FunctionId));

                result.Data = true;
                result.Message = ErrorMessages.PUT.Updated;

                if (request.RoleId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Vai trò");
                    return result;
                }

                if (request.FunctionId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Thao tác người dùng");
                    return result;
                }

                if (data == null)
                {
                    var entity = base.MapToEntity(request);
                    EntityHelper.TrySetId(entity, GuidGenerator.Create);
                    entity.IsAllowed = true;
                    await _repository.InsertAsync(entity);
                }
                else
                {
                    if (request.RoleId != data.RoleId && request.FunctionId != data.FunctionId)
                    {
                        result.Data = false;
                        result.Message = string.Format(ErrorMessages.VALID.Existed, "Vai trò cùng Thao tác người dùng");
                        return result;
                    }

                    base.MapToEntity(request, data);
                    await _repository.UpdateAsync(data);
                }
                return result;
            }
            catch (Exception)
            {
                result.Data = false;
                result.Message = ErrorMessages.PUT.CannotUpdate;
                result.Status = 400;
                return result;
            }
        }
    }
}
