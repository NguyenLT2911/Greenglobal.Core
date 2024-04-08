using Greenglobal.Core.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface IUserService :
        ICrudAppService<
            UserResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            UserRequest,
            UserRequest>
    {
        Task<BaseResponse<bool>> CreateUserAsync(UserRequest request);

        Task<BaseResponse<bool>> UpdateUserAsync(Guid id, UserRequest request);

        Task<BaseResponse<bool>> DeleteUserAsync(Guid id);

        Task<PageBaseResponse<UserResponse>> GetListUserAsync(PageBaseRequest pageRequest, SearchUserRequest request);

        Task<BaseResponse<UserResponse>> GetByIdAync(Guid id);
    }
}
