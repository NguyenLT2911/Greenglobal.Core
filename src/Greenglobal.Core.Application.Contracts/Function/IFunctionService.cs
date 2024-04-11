using Greenglobal.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface IFunctionService :
        ICrudAppService<
            FunctionResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            FunctionRequest,
            FunctionRequest>
    {
        Task<BaseResponse<bool>> CreateFunctionAsync(FunctionRequest request);

        Task<BaseResponse<bool>> UpdateFunctionAsync(Guid id, FunctionRequest request);

        Task<BaseResponse<bool>> DeleteFunctionAsync(Guid id);

        Task<BaseResponse<FunctionResponse>> GetByIdAsync(Guid id);

        Task<List<FunctionResponse>> GetHierarchyByApplicationId(Guid applicationId);

        Task<List<FunctionResponse>> GetHierarchyHavePermissionByApplication(Guid applicationId);
    }
}
