using AutoMapper;
using Greenglobal.Core.Entities;
using Greenglobal.Core.Helpers;
using Greenglobal.Core.Models;

namespace Greenglobal.Core;

public class CoreApplicationAutoMapperProfile : Profile
{
    public CoreApplicationAutoMapperProfile()
    {
        CreateMap<Unit, UnitResponse>().IgnoreAllNonExisting();
        CreateMap<UnitRequest, Unit>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<Department, DepartmentResponse>().IgnoreAllNonExisting();
        CreateMap<DepartmentRequest, Department>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<Function, FunctionResponse>().IgnoreAllNonExisting();
        CreateMap<FunctionRequest, Function>().IgnoreAllNonExisting();

        CreateMap<Application, ApplicationResponse>().IgnoreAllNonExisting();
        CreateMap<ApplicationRequest, Application>().IgnoreAllNonExisting();

        CreateMap<User, UserResponse>().IgnoreAllNonExisting();
        CreateMap<UserRequest, User>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<Title, TitleResponse>().IgnoreAllNonExisting();
        CreateMap<TitleRequest, Title>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<Role, RoleResponse>().IgnoreAllNonExisting();
        CreateMap<RoleRequest, Role>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<Permission, PermissionResponse>().IgnoreAllNonExisting();
        CreateMap<PermissionRequest, Permission>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<UserTitleDept, UserTitleDeptResponse>().IgnoreAllNonExisting();
        CreateMap<UserTitleDeptRequest, UserTitleDept>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<UserRoleApp, UserRoleAppResponse>().IgnoreAllNonExisting();
        CreateMap<UserRoleAppRequest, UserRoleApp>(MemberList.Source).IgnoreAllNonExisting();
    }
}
