using Greenglobal.Core.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface IPermissionService :
        ICrudAppService<
            PermissionResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            PermissionRequest,
            PermissionRequest>
    {
        Task<BaseResponse<bool>> CreatePermissionAsync(PermissionRequest request);

        Task<BaseResponse<bool>> UpdatePermissionAsync(Guid id, PermissionRequest request);

        Task<PageBaseResponse<PermissionResponse>> GetListPermissionAsync(PageBaseRequest pageRequest, SearchBaseRequest request);

        Task<BaseResponse<PermissionResponse>> GetByIdAync(Guid id);
    }
}
