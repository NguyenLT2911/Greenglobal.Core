using Greenglobal.Core.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface IUnitService :
        ICrudAppService<
            UnitResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            UnitRequest,
            UnitRequest>
    {
        Task<BaseResponse<bool>> CreateUnitAsync(UnitRequest request);

        Task<BaseResponse<bool>> UpdateInitAsync(Guid id, UnitRequest request);

        Task<BaseResponse<bool>> DeleteUnitAsync(Guid id);

        Task<PageBaseResponse<UnitResponse>> GetListUnitAsync(PageBaseRequest pageRequest, SearchBaseRequest request);

        Task<BaseResponse<UnitResponse>> GetByIdAync(Guid id);

        Task<BaseResponse<UnitResponse>> GetByIdMultiLevelAync(Guid id);

        Task<BaseResponse<UnitResponse>> GetByIdMultiLevelHaveDepartmentAync(Guid id);
    }
}
