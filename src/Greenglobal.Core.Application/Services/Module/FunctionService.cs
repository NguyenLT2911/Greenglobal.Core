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
    public class FunctionService :
        CrudAppService<
        Function,
        FunctionResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        FunctionRequest,
        FunctionRequest>,
        IFunctionService
    {
        private readonly IFunctionRepository _repository;
        private readonly IPermissionRepository _permissionRepository;

        public FunctionService(IFunctionRepository repository,
            IPermissionRepository permissionRepository) : base(repository)
        {
            _repository = repository;
            _permissionRepository = permissionRepository;
        }

        public async Task<BaseResponse<bool>> CreateFunctionAsync(FunctionRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.POST.Created;

                if (request.ApplicationId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Ứng dụng");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Code))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Mã chức năng");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên chức năng");
                    return result;
                }

                if (await _repository.IsDupplicationName(request.Name, request.ParentId, request.ApplicationId))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên chức năng");
                    return result;
                }

                if (await _repository.IsDupplicationCode(request.Code, request.ParentId, request.ApplicationId))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Mã chức năng");
                    return result;
                }

                var maxSortOrder = _repository.GetMaxSortOrder(request.ParentId);
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

        public async Task<BaseResponse<bool>> UpdateFunctionAsync(Guid id, FunctionRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.PUT.Updated;
                if (request.ApplicationId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Ứng dụng");
                    return result;
                }

                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Chức năng");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên chức năng");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Code))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Mã chức năng");
                    return result;
                }

                if (request.Name != entity?.Name && await _repository.IsDupplicationName(request.Name, request.ParentId, request.ApplicationId))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên chức năng");
                    return result;
                }

                if (request.Code != entity?.Code && await _repository.IsDupplicationName(request.Code, request.ParentId, request.ApplicationId))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Mã chức năng");
                    return result;
                }

                base.MapToEntity(request, entity);
                await _repository.UpdateAsync(entity);
                return result;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.Message = ErrorMessages.PUT.CannotUpdate;
                result.Status = 400;
                return result;
            }
        }

        public async Task<BaseResponse<bool>> DeleteFunctionAsync(Guid id)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;

                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Chức năng");
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

        public async Task<PageBaseResponse<FunctionResponse>> GetListFunctionAsync(PageBaseRequest pageRequest, SearchBaseRequest request)
        {
            var result = new PageBaseResponse<FunctionResponse>();
            try
            {
                var query = _repository.GetListFunction(request.Status, true, null);
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = _repository.SearchKeyword(query, request.Name);
                }
                query = query.OrderBy(x => x.SortOrder);

                result.PageNumber = pageRequest.PageNumber;
                result.TotalRow = await AsyncExecuter.CountAsync(query);
                result.Data = ObjectMapper.Map<List<Function>, List<FunctionResponse>>
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

        public async Task<BaseResponse<FunctionResponse>> GetByIdAsync(Guid id)
        {
            var result = new BaseResponse<FunctionResponse>();
            try
            {
                var entity = await AsyncExecuter.FirstOrDefaultAsync(_repository.GetById(id));
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Chức năng");
                    return result;
                }
                result.Data = ObjectMapper.Map<Function, FunctionResponse>(entity);
                if (entity.ParentId.HasValue)
                {
                    var dataParent = await AsyncExecuter.FirstOrDefaultAsync(_repository.GetById(entity.ParentId.Value));
                    if (dataParent != null)
                        result.Data.Parent = ObjectMapper.Map<Function, FunctionResponse>(dataParent);
                }

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

        /// <summary>
        /// Get data have children multi level by applicationId for response
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public async Task<List<FunctionResponse>> GetHierarchyByApplicationId(Guid applicationId)
        {
            var result = ObjectMapper.Map<List<Function>, List<FunctionResponse>>(await AsyncExecuter.ToListAsync(_repository.GetByApplicationId(applicationId)));
            if (result.Any())
            {
                foreach (var child in result)
                {
                    child.Children = await GetHierarchy(child.Id);
                }
            }
            return result;
        }

        /// <summary>
        /// Get data have children multi level have permission by applicationId for response
        /// </summary>
        /// <param name="ApplicationId"></param>
        /// <returns></returns>
        public async Task<List<FunctionResponse>> GetHierarchyHavePermissionByApplication(Guid applicationId)
        {
            var result = ObjectMapper.Map<List<Function>, List<FunctionResponse>>(await AsyncExecuter.ToListAsync(_repository.GetByApplicationId(applicationId)));
            if (result.Any())
            {
                var lstPermission = ObjectMapper.Map<List<Permission>, List<PermissionResponse>>
                    (await AsyncExecuter.ToListAsync(_permissionRepository.GetByFunctionIds(result.Select(x => x.Id).ToList())));
                foreach (var child in result)
                {
                    if (lstPermission != null && lstPermission.Any())
                        child.Permissions = lstPermission.Where(per => per.FunctionId == child.Id).ToList();
                    child.Children = await GetHierarchyHavePermissionByApplication(child.Id);
                }
            }
            return result;
        }

        /// <summary>
        /// Get data have children multi level for response
        /// </summary>
        /// <param name="request"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task<List<FunctionResponse>> GetHierarchy(Guid id)
        {
            var result = ObjectMapper.Map<List<Function>, List<FunctionResponse>>(await AsyncExecuter.ToListAsync(_repository.GetByParentId(id)));
            if (result.Any())
            {
                foreach (var child in result)
                {
                    child.Children = await GetHierarchy(child.Id);
                }
            }
            return result;
        }
    }
}
