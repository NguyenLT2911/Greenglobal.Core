using Greenglobal.Core.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core.Services
{
    public interface ITitleService :
        ICrudAppService<
            TitleResponse,
            Guid,
            PagedAndSortedResultRequestDto,
            TitleRequest,
            TitleRequest>
    {
        Task<BaseResponse<bool>> CreateTitleAsync(TitleRequest request);

        Task<BaseResponse<bool>> UpdateTitleAsync(Guid id, TitleRequest request);

        Task<BaseResponse<bool>> DeleteTitleAsync(Guid id);

        Task<PageBaseResponse<TitleResponse>> GetListTitleAsync(PageBaseRequest pageRequest, SearchBaseRequest request);

        Task<BaseResponse<TitleResponse>> GetByIdAsync(Guid id);
    }
}
