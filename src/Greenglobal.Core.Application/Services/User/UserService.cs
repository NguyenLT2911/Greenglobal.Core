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
using Volo.Abp.ObjectMapping;

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
        private readonly IUserTitleRepository _userTitleDeptRepository;
        private readonly IUserRoleAppRepository _userRoleAppRepository;

        public UserService(IUserRepository repository,
            IUserTitleRepository userTitleDeptRepository,
            IUserRoleAppRepository userRoleAppRepository) : base(repository)
        {
            _repository = repository;
            _userTitleDeptRepository = userTitleDeptRepository;
            _userRoleAppRepository = userRoleAppRepository;
        }

        public async Task<BaseResponse<bool>> CreateUserAsync(UserRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.POST.Created;

                #region Valid request
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

                if (request.TitleId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Chức vụ");
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
                #endregion

                var maxSortOrder = _repository.GetMaxSortOrder();
                maxSortOrder += 1;
                request.SortOrder = maxSortOrder;
                var entity = base.MapToEntity(request);
                EntityHelper.TrySetId(entity, GuidGenerator.Create);
                entity.Status = 1;
                //HashPassword
                entity.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var lstUserTitle = new List<UserTitleDept>();
                //Set Title main
                var userTitleMain = new UserTitleDept();
                userTitleMain.DepartmentId = request.DepartmentId;
                userTitleMain.TitleId = request.TitleId;
                userTitleMain.IsMain = true;
                lstUserTitle.Add(userTitleMain);
                //Set Title not main
                if (request.Concurrent != null && request.Concurrent.Any())
                    lstUserTitle.AddRange(ObjectMapper.Map<List<UserTitleDeptRequest>, List<UserTitleDept>>(request.Concurrent));
                lstUserTitle.ForEach(x => x.UserId = entity.Id);

                //Set List User use App with Role
                var lstUserRoleApp = new List<UserRoleApp>();
                if (request.UserRoleApps != null && request.UserRoleApps.Any())
                    lstUserRoleApp.AddRange(ObjectMapper.Map<List<UserRoleAppRequest>, List<UserRoleApp>>(request.UserRoleApps));
                lstUserRoleApp.ForEach(x => x.UserId = entity.Id);

                await _repository.InsertAsync(entity);
                await _userTitleDeptRepository.InsertManyAsync(lstUserTitle, true);
                await _userRoleAppRepository.InsertManyAsync(lstUserRoleApp, true);
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

        public async Task<BaseResponse<bool>> UpdateUserAsync(Guid id, UserRequest request)
        {
            var result = new BaseResponse<bool>();
            try
            {
                result.Data = true;
                result.Message = ErrorMessages.PUT.Updated;
                var entity = await _repository.GetAsync(id);

                #region Valid request
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

                if (request.TitleId == Guid.Empty)
                {
                    result.Data = false;
                    result.Message = string.Format(ErrorMessages.VALID.RequiredField, "Chức vụ");
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
                #endregion

                var maxSortOrder = _repository.GetMaxSortOrder();
                base.MapToEntity(request, entity);
                entity.SortOrder = maxSortOrder++;
                entity.UpdatedAt = DateTime.UtcNow;

                var lstUserTitle = new List<UserTitleDept>();
                //Set Title main
                var userTitleMain = new UserTitleDept();
                userTitleMain.DepartmentId = request.DepartmentId;
                userTitleMain.TitleId = request.TitleId;
                userTitleMain.IsMain = true;
                lstUserTitle.Add(userTitleMain);
                //Set Title not main
                if (request.Concurrent != null && request.Concurrent.Any())
                    lstUserTitle.AddRange(ObjectMapper.Map<List<UserTitleDeptRequest>, List<UserTitleDept>>(request.Concurrent));
                lstUserTitle.ForEach(x => x.UserId = entity.Id);

                //Set List User use App with Role
                var lstUserRoleApp = new List<UserRoleApp>();
                if (request.UserRoleApps != null && request.UserRoleApps.Any())
                    lstUserRoleApp.AddRange(ObjectMapper.Map<List<UserRoleAppRequest>, List<UserRoleApp>>(request.UserRoleApps));
                lstUserRoleApp.ForEach(x => x.UserId = entity.Id);

                //Delete Titles old
                var lstUserTitleOld = _userTitleDeptRepository.GetByUserId(id, false);
                await _userTitleDeptRepository.DeleteManyAsync(lstUserTitleOld);

                //Delete UserRoleApps old
                var lstUserRoleAppOld = _userRoleAppRepository.GetByUserId(id, false);
                await _userRoleAppRepository.DeleteManyAsync(lstUserRoleAppOld);

                //Insert Title new
                await _userTitleDeptRepository.InsertManyAsync(lstUserTitle);
                //Insert UserRoleApps new
                await _userRoleAppRepository.InsertManyAsync(lstUserRoleApp);

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
                    var lstUserTitleDepts = _userTitleDeptRepository.GetByDepartmentId(request.DepartmentId.Value);
                    query = query.Where(x => lstUserTitleDepts.Contains(x.Id));
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

        public async Task<BaseResponse<UserResponse>> GetByIdAsync(Guid id)
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
                var lstUserTitleDepts = ObjectMapper.Map<List<UserTitleDept>, List<UserTitleDeptResponse>>
                    (await AsyncExecuter.ToListAsync(_userTitleDeptRepository.GetByUserId(id)));
                if (lstUserTitleDepts.Any())
                {
                    var userTitleMain = lstUserTitleDepts.FirstOrDefault(x => x.IsMain);
                    if (userTitleMain != null)
                    {
                        result.Data.Title = userTitleMain.Title;
                        result.Data.Department = userTitleMain.Department;
                    }
                    result.Data.Concurrent = lstUserTitleDepts.Where(x => !x.IsMain).ToList();
                }
                var lstUserRoleApp = await AsyncExecuter.ToListAsync(_userRoleAppRepository.GetByUserId(id));
                if (lstUserRoleApp != null && lstUserRoleApp.Any())
                    result.Data.UserRoleApps = ObjectMapper.Map<List<UserRoleApp>, List<UserRoleAppResponse>>(lstUserRoleApp);
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
