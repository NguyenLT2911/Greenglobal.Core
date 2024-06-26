﻿using Greenglobal.Core.Constants;
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
    public class RoleService :
        CrudAppService<
        Role,
        RoleResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        RoleRequest,
        RoleRequest>,
        IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> CreateRoleAsync(RoleRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.POST.Created;

                if (request.ApplicationId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Ứng dụg");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên vai trò");
                    return result;
                }

                if (await _repository.IsDupplication(request.Name, request.ApplicationId))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Ứng dụng và Vai trò");
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
            catch (Exception)
            {
                result.Data = false;
                result.Message = ErrorMessages.POST.CannotCreate;
                result.Status = 400;
                return result;
            }
        }

        public async Task<BaseResponse<bool>> UpdateRoleAsync(Guid id, RoleRequest request)
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
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Vai trò");
                    return result;
                }

                if (request.ApplicationId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Ứng dụg");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên vai trò");
                    return result;
                }

                if (request.Name != entity?.Name && request.ApplicationId != entity.ApplicationId
                    && await _repository.IsDupplication(request.Name, request.ApplicationId))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Ứng dụng và Vai trò");
                    return result;
                }

                base.MapToEntity(request, entity);
                entity.UpdatedAt = DateTime.UtcNow;

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

        public async Task<BaseResponse<bool>> DeleteRoleAsync(Guid id)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Vai trò");
                    return result;
                }
                entity.Status = -1;
                entity.UpdatedAt = DateTime.UtcNow;

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

        public async Task<PageBaseResponse<RoleResponse>> GetListRoleAsync(PageBaseRequest pageRequest, SearchBaseRequest request)
        {
            var result = new PageBaseResponse<RoleResponse>();
            try
            {
                var query = _repository.GetListRole(request.Status);
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = _repository.SearchKeyword(query, request.Name);
                }
                query = query.OrderBy(x => x.SortOrder);

                result.PageNumber = pageRequest.PageNumber;
                result.TotalRow = await AsyncExecuter.CountAsync(query);
                result.Data = ObjectMapper.Map<List<Role>, List<RoleResponse>>
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

        public async Task<BaseResponse<RoleResponse>> GetByIdAsync(Guid id)
        {
            var result = new BaseResponse<RoleResponse>();
            try
            {
                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Vai trò");
                    return result;
                }
                result.Data = ObjectMapper.Map<Role, RoleResponse>(entity);
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
