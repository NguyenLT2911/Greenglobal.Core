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

                var lstFunction = GetDataMultiLevel(request, entity);
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
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Ứng dụng");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên ứng dụng");
                    return result;
                }

                if (request.Name != entity?.Name && await _repository.IsDupplicationName(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên ứng dụng");
                    return result;
                }

                //Find children multiple level then delete it
                var lstFunctionOld = new List<Function>();

                var lstFunctionChildA = _repository.GetByParentId(id);
                var lstFunctionChildB = _repository.GetByParentIds(lstFunctionChildA.Select(x => x.Id).ToList());
                var lstFunctionChildC = _repository.GetByParentIds(lstFunctionChildB.Select(x => x.Id).ToList());

                lstFunctionOld.Add(entity);
                lstFunctionOld.AddRange(lstFunctionChildA);
                lstFunctionOld.AddRange(lstFunctionChildB);
                lstFunctionOld.AddRange(lstFunctionChildC);
                await _repository.DeleteManyAsync(lstFunctionOld, true);

                //Insert data new
                base.MapToEntity(request, entity);
                var lstFunctionNew = GetDataMultiLevel(request, entity);
                await _repository.InsertManyAsync(lstFunctionNew);
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

        public async Task<BaseResponse<FunctionResponse>> GetByIdAync(Guid id)
        {
            var result = new BaseResponse<FunctionResponse>();
            try
            {
                var entity = await AsyncExecuter.FirstOrDefaultAsync(_repository.GetById(id));
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Ứng dụng");
                    return result;
                }
                result.Data = ObjectMapper.Map<Function, FunctionResponse>(entity);
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

        /// <summary>
        /// Get data have children multi level for entity to insert or update
        /// </summary>
        /// <param name="request"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private List<Function> GetDataMultiLevel(FunctionRequest request, Function entity)
        {
            var lstFunction = new List<Function>();
            EntityHelper.TrySetId(entity, GuidGenerator.Create);
            lstFunction.Add(entity);

            if (request.Children != null && request.Children.Any())
            {
                int indexA = 0;
                foreach (var childA in request.Children)
                {
                    var childAEntity = ObjectMapper.Map<FunctionRequest, Function>(childA);

                    indexA++;
                    EntityHelper.TrySetId(childAEntity, GuidGenerator.Create);
                    childAEntity.ParentId = entity.Id;
                    childAEntity.SortOrder = indexA;
                    lstFunction.Add(childAEntity);
                    if (childA.Children != null && childA.Children.Any())
                    {
                        int indexB = 0;
                        foreach (var childB in childA.Children)
                        {
                            var childBEntity = ObjectMapper.Map<FunctionRequest, Function>(childB);

                            indexB++;
                            EntityHelper.TrySetId(childBEntity, GuidGenerator.Create);
                            childBEntity.ParentId = childAEntity.Id;
                            childBEntity.IsModule = false;
                            childBEntity.SortOrder = indexB;
                            lstFunction.Add(childBEntity);
                            if (childB.Children != null && childB.Children.Any())
                            {
                                int indexC = 0;
                                foreach (var childC in childB.Children)
                                {
                                    var childCEntity = ObjectMapper.Map<FunctionRequest, Function>(childC);

                                    indexC++;
                                    EntityHelper.TrySetId(childCEntity, GuidGenerator.Create);
                                    childCEntity.ParentId = childBEntity.Id;
                                    childCEntity.IsModule = false;
                                    childCEntity.SortOrder = indexC;
                                    lstFunction.Add(childCEntity);
                                }
                            }
                        }
                    }
                }
            }
            return lstFunction;
        }
    }
}
