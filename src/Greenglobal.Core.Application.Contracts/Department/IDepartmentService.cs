using Greenglobal.Core.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface IDepartmentService :
        ICrudAppService<
            DepartmentResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            DepartmentRequest,
            DepartmentRequest>
    {
        Task<BaseResponse<bool>> CreateDepartmentAsync(DepartmentRequest request);

        Task<BaseResponse<bool>> UpdateDepartmentAsync(Guid id, DepartmentRequest request);

        Task<BaseResponse<bool>> DeleteDepartmentAsync(Guid id);

        Task<PageBaseResponse<DepartmentResponse>> GetListDepartmentAsync(PageBaseRequest pageRequest, SearchBaseRequest request);

        Task<BaseResponse<DepartmentResponse>> GetByIdAync(Guid id);

        Task<BaseResponse<DepartmentResponse>> GetByIdMultiLevelAync(Guid id);
    }
}
