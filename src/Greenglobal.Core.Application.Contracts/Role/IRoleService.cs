using Greenglobal.Core.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface IRoleService :
        ICrudAppService<
            RoleResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            RoleRequest,
            RoleRequest>
    {
        Task<BaseResponse<bool>> CreateRoleAsync(RoleRequest request);

        Task<BaseResponse<bool>> UpdateRoleAsync(Guid id, RoleRequest request);

        Task<BaseResponse<bool>> DeleteRoleAsync(Guid id);

        Task<PageBaseResponse<RoleResponse>> GetListRoleAsync(PageBaseRequest pageRequest, SearchBaseRequest request);

        Task<BaseResponse<RoleResponse>> GetByIdAync(Guid id);
    }
}
