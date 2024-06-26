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
using Volo.Abp.ObjectMapping;

namespace Greenglobal.Core.Services
{
    [RemoteService(IsMetadataEnabled = false)]
    public class UnitService :
        CrudAppService<
        Unit,
        UnitResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        UnitRequest,
        UnitRequest>,
        IUnitService
    {
        private readonly IUnitRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentService _departmentService;

        public UnitService(IUnitRepository repository,
            IDepartmentRepository departmentRepository,
            IDepartmentService departmentService) : base(repository)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
            _departmentService = departmentService;
        }

        public async Task<BaseResponse<bool>> CreateUnitAsync(UnitRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.POST.Created;

                if (string.IsNullOrEmpty(request.ShortNamne))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên viết tắt đơn vị");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên đơn vị");
                    return result;
                }

                if (await _repository.IsDupplicationName(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên đơn vị");
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
            catch (Exception)
            {
                result.Data = false;
                result.Message = ErrorMessages.POST.CannotCreate;
                result.Status = 400;
                return result;
            }
        }

        public async Task<BaseResponse<bool>> UpdateInitAsync(Guid id, UnitRequest request)
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
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Đơn vị");
                    return result;
                }

                if (string.IsNullOrEmpty(request.ShortNamne))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên viết tắt đơn vị");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên đơn vị");
                    return result;
                }

                if (request.Name != entity?.Name && await _repository.IsDupplicationName(request.Name))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên đơn vị");
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

        public async Task<BaseResponse<bool>> DeleteUnitAsync(Guid id)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;

                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Đơn vị");
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

        public async Task<PageBaseResponse<UnitResponse>> GetListUnitAsync(PageBaseRequest pageRequest, SearchBaseRequest request)
        {
            var result = new PageBaseResponse<UnitResponse>();
            try
            {
                var query = _repository.GetListUnit(request.Status);
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = _repository.SearchKeyword(query, request.Name);
                }
                query = query.OrderBy(x => x.SortOrder);

                result.PageNumber = pageRequest.PageNumber;
                result.TotalRow = await AsyncExecuter.CountAsync(query);
                result.Data = ObjectMapper.Map<List<Unit>, List<UnitResponse>>
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

        public async Task<BaseResponse<UnitResponse>> GetByIdAsync(Guid id)
        {
            var result = new BaseResponse<UnitResponse>();
            try
            {
                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Đơn vị");
                    return result;
                }
                result.Data = ObjectMapper.Map<Unit, UnitResponse>(entity);
                //Get unit parent when this unit is child
                if (result.Data.ParentId.HasValue)
                {
                    result.Data.Parent = ObjectMapper.Map<Unit, UnitResponse>(await _repository.GetAsync(result.Data.ParentId.Value));
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

        public async Task<BaseResponse<UnitResponse>> GetByIdMultiLevelAync(Guid id)
        {
            var result = new BaseResponse<UnitResponse>();
            try
            {
                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Đơn vị");
                    return result;
                }
                result.Data = ObjectMapper.Map<Unit, UnitResponse>(entity);
                //Get unit parent when this unit is child
                if (result.Data.ParentId.HasValue)
                {
                    result.Data.Parent = ObjectMapper.Map<Unit, UnitResponse>(await _repository.GetAsync(result.Data.ParentId.Value));
                }
                //Get Hierarchy unit children when this unit is parent
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

        public async Task<BaseResponse<UnitResponse>> GetByIdMultiLevelHaveDepartmentAync(Guid id)
        {
            var result = new BaseResponse<UnitResponse>();
            try
            {
                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Đơn vị");
                    return result;
                }
                result.Data = ObjectMapper.Map<Unit, UnitResponse>(entity);
                //Get unit parent when this unit is child
                if (result.Data.ParentId.HasValue)
                {
                    result.Data.Parent = ObjectMapper.Map<Unit, UnitResponse>(await _repository.GetAsync(result.Data.ParentId.Value));
                }
                //Get Hierarchy unit children when this unit is parent
                result.Data.Children = await GetHierarchy(id, true);

                //Get Hierarchy department from department parent of unit
                var lstDepartment = _departmentRepository.GetByUnitId(id);
                lstDepartment = lstDepartment.Where(x => !x.ParentId.HasValue);
                var departmentParent = ObjectMapper.Map<List<Department>, List<DepartmentResponse>>(await AsyncExecuter.ToListAsync(lstDepartment));
                result.Data.Departments = new List<DepartmentResponse>();
                if (departmentParent != null && departmentParent.Any())
                {
                    foreach (var department in departmentParent)
                    {
                        department.Children = await _departmentService.GetHierarchy(department.Id);
                        result.Data.Departments.Add(department);
                    }
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

        private async Task<List<UnitResponse>> GetHierarchy(Guid id, bool haveDepartment = false)
        {
            var result = ObjectMapper.Map<List<Unit>, List<UnitResponse>>(await AsyncExecuter.ToListAsync(_repository.GetByParentId(id)));
            if (result.Any())
            {
                var lstUnitIds = result.Select(x => x.Id).ToList();

                //Get Hierarchy department from department parent of unit
                var lstDepartment = _departmentRepository.GetByUnitIds(lstUnitIds);
                lstDepartment = lstDepartment.Where(x => !x.ParentId.HasValue);
                var departmentParent = ObjectMapper.Map<List<Department>, List<DepartmentResponse>>(await AsyncExecuter.ToListAsync(lstDepartment));

                foreach (var child in result)
                {
                    if (haveDepartment && departmentParent.Any())
                    {
                        child.Departments = new List<DepartmentResponse>();
                        //Get lít department by unit
                        departmentParent = departmentParent.Where(x => x.UnitId == child.Id).ToList();
                        if (departmentParent.Any())
                        {
                            foreach (var department in departmentParent)
                            {
                                department.Children = await _departmentService.GetHierarchy(department.Id);
                                child.Departments.Add(department);
                            }
                        }
                    }
                    child.Children = await GetHierarchy(child.Id);
                }
            }
            return result;
        }
    }
}
