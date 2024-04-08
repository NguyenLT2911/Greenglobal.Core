using Greenglobal.Core.Constants;
using Greenglobal.Core.Entities;
using Greenglobal.Core.Models;
using Greenglobal.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
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
        private readonly IUserRoleDeptRepository _userRoleDeptRepository;

        public UserService(IUserRepository repository,
            IUserRoleDeptRepository userRoleDeptRepository) : base(repository)
        {
            _repository = repository;
            _userRoleDeptRepository = userRoleDeptRepository;
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

                if (!Regex.IsMatch(request.Email, Consts.Regex.FormatEmail, RegexOptions.IgnoreCase))
                {
                    result.Data = false;
                    result.Message = ErrorMessages.VALID.NotEmailFormat;
                    return result;
                }

                if (await _repository.IsDupplicationUserName(request.UserName))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên đăng nhập");
                    return result;
                }

                if (await _repository.IsDupplicationEmail(request.Email))
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
                //HashPassword
                entity.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var lstUserRole = new List<UserRoleDept>();
                //Set Role main
                var userRoleMain = new UserRoleDept();
                userRoleMain.DepartmentId = request.DepartmentId;
                userRoleMain.RoleId = request.RoleId;
                userRoleMain.IsMain = true;
                lstUserRole.Add(userRoleMain);
                //Set role not main
                if (request.Concurrent != null && request.Concurrent.Any())
                    lstUserRole.AddRange(ObjectMapper.Map<List<UserRoleDeptRequest>, List<UserRoleDept>>(request.Concurrent));
                lstUserRole.ForEach(x => x.UserId = entity.Id);

                await _repository.InsertAsync(entity);
                await _userRoleDeptRepository.InsertManyAsync(lstUserRole, true);
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

        public async Task<BaseResponse<bool>> UpdateUserAsync(Guid id, UserRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.PUT.Updated;

                var entity = await _repository.GetAsync(id);
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

                if (request.UserName != entity.UserName && await _repository.IsDupplicationUserName(request.UserName))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Tên đăng nhập");
                    return result;
                }

                if (request.Email != entity.Email && await _repository.IsDupplicationEmail(request.Email))
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.Existed, "Email");
                    return result;
                }

                var maxSortOrder = _repository.GetMaxSortOrder();
                base.MapToEntity(request, entity);
                entity.SortOrder = maxSortOrder++;
                entity.UpdatedAt = DateTime.UtcNow;

                var lstUserRole = new List<UserRoleDept>();
                //Set Role main
                var userRoleMain = new UserRoleDept();
                userRoleMain.DepartmentId = request.DepartmentId;
                userRoleMain.RoleId = request.RoleId;
                userRoleMain.IsMain = true;
                lstUserRole.Add(userRoleMain);
                //Set role not main
                if (request.Concurrent != null && request.Concurrent.Any())
                    lstUserRole.AddRange(ObjectMapper.Map<List<UserRoleDeptRequest>, List<UserRoleDept>>(request.Concurrent));
                lstUserRole.ForEach(x => x.UserId = entity.Id);

                //Delete roles old
                var lstUserRoleOld = _userRoleDeptRepository.GetByUserId(id, false);
                await _userRoleDeptRepository.DeleteManyAsync(lstUserRoleOld);
                //Insert role new
                await _userRoleDeptRepository.InsertManyAsync(lstUserRole);
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

        public async Task<BaseResponse<bool>> DeleteUserAsync(Guid id)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;

                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Người dùng");
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

        public async Task<PageBaseResponse<UserResponse>> GetListUserAsync(PageBaseRequest pageRequest, SearchUserRequest request)
        {
            var result = new PageBaseResponse<UserResponse>();
            try
            {
                var query = _repository.GetListUser(request.Status);

                if (request.DepartmentId.HasValue)
                {
                    var lstUserRoleDepts = _userRoleDeptRepository.GetByDepartmentId(request.DepartmentId.Value);
                    query = query.Where(x => lstUserRoleDepts.Contains(x.Id));
                }

                if (!string.IsNullOrEmpty(request.FullName))
                {
                    query = _repository.SearchKeyword(query, request.FullName);
                }
                query = query.OrderBy(x => x.SortOrder);

                result.PageNumber = pageRequest.PageNumber;
                result.TotalRow = await AsyncExecuter.CountAsync(query);
                result.Data = ObjectMapper.Map<List<User>, List<UserResponse>>
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

        public async Task<BaseResponse<UserResponse>> GetByIdAync(Guid id)
        {
            var result = new BaseResponse<UserResponse>();
            try
            {
                var entity = await _repository.GetAsync(id);
                if (entity == null)
                {
                    result.Message = string.Format(ErrorMessages.VALID.NotExisted, "Người dùng");
                    return result;
                }
                result.Data = ObjectMapper.Map<User, UserResponse>(entity);
                var lstUserRoleDepts = ObjectMapper.Map<List<UserRoleDept>, List<UserRoleDeptResponse>>
                    (await AsyncExecuter.ToListAsync(_userRoleDeptRepository.GetByUserId(id)));
                if (lstUserRoleDepts.Any())
                {
                    var userRoleMain = lstUserRoleDepts.FirstOrDefault(x => x.IsMain);
                    if (userRoleMain != null)
                    {
                        result.Data.Role = userRoleMain.Role;
                        result.Data.Department = userRoleMain.Department;
                    }
                    result.Data.UserRoleDepts = lstUserRoleDepts.Where(x => !x.IsMain).ToList();
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


    }
}
