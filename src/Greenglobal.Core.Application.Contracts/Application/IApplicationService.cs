using Greenglobal.Core.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface IApplicationService :
        ICrudAppService<
            ApplicationResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            ApplicationRequest,
            ApplicationRequest>
    {
        Task<BaseResponse<bool>> CreateApplicationAsync(ApplicationRequest request);

        Task<BaseResponse<bool>> UpdateApplicationAsync(Guid id, ApplicationRequest request);

        Task<BaseResponse<bool>> DeleteApplicationAsync(Guid id);

        Task<PageBaseResponse<ApplicationResponse>> GetListApplicationAsync(PageBaseRequest pageRequest, SearchBaseRequest request);

        Task<BaseResponse<ApplicationResponse>> GetByIdAsync(Guid id);
    }
}
