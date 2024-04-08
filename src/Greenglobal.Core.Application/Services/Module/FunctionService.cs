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

        public FunctionService(IFunctionRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> CreateFunctionAsync(FunctionRequest request)
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

                if (await _repository.IsDupplicationName(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên ứng dụng");
                    return result;
                }
                var maxSortOrder = _repository.GetMaxSortOrder(null);
                maxSortOrder += 1;

                request.SortOrder = maxSortOrder;
                var entity = base.MapToEntity(request);
                EntityHelper.TrySetId(entity, GuidGenerator.Create);
                entity.Status = 1;

                var lstFunction = new List<Function>();
                lstFunction.Add(entity);

                if (request.Children != null && request.Children.Any())
                {
                    var lstChildren = ObjectMapper.Map<List<FunctionRequest>, List<Function>>(request.Children);
                    foreach(var childA in lstChildren)
                    {
                        EntityHelper.TrySetId(childA, GuidGenerator.Create);
                        childA.ParentId = entity.Id;
                        childA.IsModule = true;
                        lstFunction.Add(childA);
                        //foreach(var childB in childA.Children)
                        //{
                        //    EntityHelper.TrySetId(childB, GuidGenerator.Create);
                        //    childB.ParentId = childA.Id;
                        //    lstFunction.Add(childB);
                        //    foreach (var childC in childB.Children)
                        //    {
                        //        EntityHelper.TrySetId(childC, GuidGenerator.Create);
                        //        childC.ParentId = childB.Id;
                        //        lstFunction.Add(childC);
                        //    }
                        //}
                    }
                }
                //lstFunction.ForEach(x => x.Children = null);
                await _repository.InsertManyAsync(lstFunction);
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

                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, entity.IsModule ? "Ứng dụng" : "Chức năng");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, entity.IsModule ? "Tên ứng dụng" : "Tên chức năng");
                    return result;
                }

                if (request.Name != entity?.Name && await _repository.IsDupplicationName(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, entity.IsModule ? "Tên ứng dụng" : "Tên chức năng");
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
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, entity.IsModule ? "Ứng dụng" : "Chức năng");
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
                var query = _repository.GetListFunction(request.Status);
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
            catch (Exception)
            {
                result.Message = ErrorMessages.GET.GetFail;
                result.Status = 400;
                return result;
            }
        }

        public async Task<BaseResponse<FunctionResponse>> GetByIdAync(Guid id)
        {
            var result = new BaseResponse<FunctionResponse>();
            try
            {
                var entity = await AsyncExecuter.FirstOrDefaultAsync(_repository.GetById(id));
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, entity.IsModule ? "Ứng dụng" : "Chức năng");
                    return result;
                }
                result.Data = ObjectMapper.Map<Function, FunctionResponse>(entity);
                //Get Function parent when this Function is child
                //if (result.Data.ParentId.HasValue)
                //{
                //    result.Data.Parent = ObjectMapper.Map<Function, FunctionResponse>(await _repository.GetAsync(result.Data.ParentId.Value));
                //}

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

        public async Task<BaseResponse<FunctionResponse>> GetByIdMultiLevelAync(Guid id)
        {
            var result = new BaseResponse<FunctionResponse>();
            try
            {
                var entity = await AsyncExecuter.FirstOrDefaultAsync(_repository.GetById(id));
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, entity.IsModule ? "Ứng dụng" : "Chức năng");
                    return result;
                }
                result.Data = ObjectMapper.Map<Function, FunctionResponse>(entity);
                //Get Function parent when this Function is child
                //if (result.Data.ParentId.HasValue)
                //{
                //    result.Data.Parent = ObjectMapper.Map<Function, FunctionResponse>(await _repository.GetAsync(result.Data.ParentId.Value));
                //}
                //Get Hierarchy Function children when this Function is parent
                //else
                result.Data.Children = await GetHierarchy(id);

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
