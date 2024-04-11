using Greenglobal.Core.Constants;
using Greenglobal.Core.Entities;
using Greenglobal.Core.Models;
using Greenglobal.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace Greenglobal.Core.Services
{
    [RemoteService(IsMetadataEnabled = false)]
    public class ApplicationService :
        CrudAppService<
        Application,
        ApplicationResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        ApplicationRequest,
        ApplicationRequest>,
        IApplicationService
    {
        private readonly IApplicationRepository _repository;
        private readonly IFunctionService _functionService;

        public ApplicationService(IApplicationRepository repository,
            IFunctionService functionService) : base(repository)
        {
            _repository = repository;
            _functionService = functionService;
        }

        public async Task<BaseResponse<bool>> CreateApplicationAsync(ApplicationRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.POST.Created;

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên ứng dụng");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Code))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Mã ứng dụng");
                    return result;
                }

                if (await _repository.IsDupplicationName(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên ứng dụng");
                    return result;
                }

                if (await _repository.IsDupplicationCode(request.Code))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Mã ứng dụng");
                    return result;
                }

                var maxSortOrder = _repository.GetMaxSortOrder();
                maxSortOrder += 1;

                request.SortOrder = maxSortOrder;
                var entity = base.MapToEntity(request);
                EntityHelper.TrySetId(entity, GuidGenerator.Create);
                entity.Status = 1;

                await _repository.InsertAsync(entity);
                return result;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.Message = ErrorMessages.POST.CannotCreate;
                result.Status = 400;
                return result;
            }
        }

        public async Task<BaseResponse<bool>> UpdateApplicationAsync(Guid id, ApplicationRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.PUT.Updated;

                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Ứng dụng");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên ứng dụng");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Code))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Mã ứng dụng");
                    return result;
                }

                if (request.Name != entity?.Name && await _repository.IsDupplicationName(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên ứng dụng");
                    return result;
                }

                if (request.Code != entity?.Code && await _repository.IsDupplicationCode(request.Code))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Mã ứng dụng");
                    return result;
                }

                base.MapToEntity(request, entity);
                await _repository.UpdateAsync(entity);
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

        public async Task<BaseResponse<bool>> DeleteApplicationAsync(Guid id)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;

                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Ứng dụng");
                    return result;
                }
                entity.Status = -1;

                await _repository.UpdateAsync(entity);
                result.Message = ErrorMessages.DELETE.Deleted;
                return result;
            }
            catch (Exception)
            {
                result.Data = false;
                result.Message = ErrorMessages.DELETE.DeletedError;
                result.Status = 400;
                return result;
            }
        }

        public async Task<PageBaseResponse<ApplicationResponse>> GetListApplicationAsync(PageBaseRequest pageRequest, SearchBaseRequest request)
        {
            var result = new PageBaseResponse<ApplicationResponse>();
            try
            {
                var query = _repository.GetListApplication(request.Status);
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = _repository.SearchKeyword(query, request.Name);
                }
                query = query.OrderBy(x => x.SortOrder);

                result.PageNumber = pageRequest.PageNumber;
                result.TotalRow = await AsyncExecuter.CountAsync(query);
                result.Data = ObjectMapper.Map<List<Application>, List<ApplicationResponse>>
                    (await AsyncExecuter.ToListAsync(query.Page(pageRequest.PageNumber, pageRequest.PageSize)));
                result.Message = ErrorMessages.GET.Getted;
                return result;
            }
            catch (Exception e)
            {
                result.Message = ErrorMessages.GET.GetFail;
                result.Status = 400;
                return result;
            }
        }

        public async Task<BaseResponse<ApplicationResponse>> GetByIdAsync(Guid id)
        {
            var result = new BaseResponse<ApplicationResponse>();
            try
            {
                var entity = await AsyncExecuter.FirstOrDefaultAsync(_repository.GetById(id));
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Ứng dụng");
                    return result;
                }
                result.Data = ObjectMapper.Map<Application, ApplicationResponse>(entity);
                result.Data.Functions = await _functionService.GetHierarchyByApplicationId(id);
                result.Message = ErrorMessages.GET.Getted;
                return result;
            }
            catch (Exception)
            {
                result.Message = ErrorMessages.GET.GetFail;
                result.Status = 400;
                return result;
            }
        }

        public async Task<BaseResponse<ApplicationResponse>> GetHavePermissionByIdAsync(Guid id)
        {
            var result = new BaseResponse<ApplicationResponse>();
            try
            {
                var entity = await AsyncExecuter.FirstOrDefaultAsync(_repository.GetById(id));
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Ứng dụng");
                    return result;
                }
                result.Data = ObjectMapper.Map<Application, ApplicationResponse>(entity);
                result.Data.Functions = await _functionService.GetHierarchyHavePermissionByApplication(id);

                result.Message = ErrorMessages.GET.Getted;
                return result;
            }
            catch (Exception)
            {
                result.Message = ErrorMessages.GET.GetFail;
                result.Status = 400;
                return result;
            }
        }
    }
}
