using Greenglobal.Core.Constants;
using Greenglobal.Core.Entities;
using Greenglobal.Core.Models;
using Greenglobal.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace Greenglobal.Core.Services
{
    [RemoteService(IsMetadataEnabled = false)]
    public class UserService :
        CrudAppService<
        User,
        UserResponse,
        Guid,
        PagedAndSortedResultRequestDto,
        UserRequest,
        UserRequest>,
        IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> CreateUserAsync(UserRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.POST.Created;

                if (string.IsNullOrEmpty(request.FullName))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên nhân viên");
                    return result;
                }

                if (string.IsNullOrEmpty(request.UserName))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên đăng nhập");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Password))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Mật khẩu");
                    return result;
                }

                if (string.IsNullOrEmpty(request.Email))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Email");
                    return result;
                }

                if (request.UnitId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Đơn vị");
                    return result;
                }

                if (request.DepartmentId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Phòng ban");
                    return result;
                }

                if (request.RoleId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Vai trò");
                    return result;
                }

                if (await _repository.IsDupplicationUserName(request.UserName))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên đăng nhập");
                    return result;
                }

                if (await _repository.IsDupplicationUserName(request.Email))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Email");
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

        //public async Task<BaseResponse<bool>> UpdateUserAsync(Guid id, UserRequest request)
        //{
        //    var result = new BaseResponse<bool>();
        //    try
        //    {
        //        result.Data = true;
        //        result.Message = ErrorMessages.PUT.Updated;

        //        var entity = await _repository.GetAsync(id);
        //        if (entity == null)
        //        {
        //            result.Data = false;
        //            result.Message = string.Format(ErrorMessages.VALID.NotExisted, "phòng ban");
        //            return result;
        //        }

        //        if (string.IsNullOrEmpty(request.Name))
        //        {
        //            result.Data = false;
        //            result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Tên phòng ban");
        //            return result;
        //        }

        //        if (request.Name != entity?.Name && await _repository.IsDupplicationName(request.Name))
        //        {
        //            result.Data = false;
        //            result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên phòng ban");
        //            return result;
        //        }
        //        var maxSortOrder = _repository.GetMaxSortOrder(request.ParentId);
        //        base.MapToEntity(request, entity);
        //        entity.SortOrder = maxSortOrder++;
        //        entity.UpdatedAt = DateTime.UtcNow;

        //        await _repository.UpdateAsync(entity);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        result.Data = false;
        //        result.Message = ErrorMessages.PUT.CannotUpdate;
        //        result.Status = 400;
        //        return result;
        //    }
        //}

        //public async Task<BaseResponse<bool>> DeleteUserAsync(Guid id)
        //{
        //    var result = new BaseResponse<bool>();
        //    try
        //    {
        //        result.Data = true;

        //        var entity = await _repository.GetAsync(id);
        //        if (entity == null)
        //        {
        //            result.Data = false;
        //            result.Message = string.Format(ErrorMessages.VALID.NotExisted, "phòng ban");
        //            return result;
        //        }
        //        entity.Status = -1;
        //        entity.UpdatedAt = DateTime.UtcNow;

        //        await _repository.UpdateAsync(entity);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        result.Data = false;
        //        result.Message = ErrorMessages.DELETE.DeletedError;
        //        result.Status = 400;
        //        return result;
        //    }
        //}

        //public async Task<PageBaseResponse<UserResponse>> GetListUserAsync(PageBaseRequest pageRequest, SearchBaseRequest request)
        //{
        //    var result = new PageBaseResponse<UserResponse>();
        //    try
        //    {
        //        var query = _repository.GetListUser(request.Status);
        //        if (!string.IsNullOrEmpty(request.Keyword))
        //        {
        //            query = _repository.SearchKeyword(query, request.Keyword);
        //        }
        //        query = query.OrderBy(x => x.SortOrder);

        //        result.PageNumber = pageRequest.PageNumber;
        //        result.TotalRow = await AsyncExecuter.CountAsync(query);
        //        result.Data = ObjectMapper.Map<List<User>, List<UserResponse>>
        //            (await AsyncExecuter.ToListAsync(query.Page(pageRequest.PageNumber, pageRequest.PageSize)));
        //        result.Message = ErrorMessages.GET.Getted;
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        result.Message = ErrorMessages.GET.GetFail;
        //        result.Status = 400;
        //        return result;
        //    }
        //}

        //public async Task<BaseResponse<UserResponse>> GetByIdAync(Guid id)
        //{
        //    var result = new BaseResponse<UserResponse>();
        //    try
        //    {
        //        var entity = await _repository.GetAsync(id);
        //        if (entity == null)
        //        {
        //            result.Message = string.Format(ErrorMessages.VALID.NotExisted, "phòng ban");
        //            return result;
        //        }
        //        result.Data = ObjectMapper.Map<User, UserResponse>(entity);
        //        //Get User parent when this User is child
        //        if (result.Data.ParentId.HasValue)
        //        {
        //            result.Data.Parent = ObjectMapper.Map<User, UserResponse>(await _repository.GetAsync(result.Data.ParentId.Value));
        //        }

        //        result.Message = ErrorMessages.GET.Getted;
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        result.Message = ErrorMessages.GET.GetFail;
        //        result.Status = 400;
        //        return result;
        //    }
        //}
    }
}
