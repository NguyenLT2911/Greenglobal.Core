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

        CreateMap<User, UserResponse>().IgnoreAllNonExisting();
        CreateMap<UserRequest, User>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<Role, RoleResponse>().IgnoreAllNonExisting();
        CreateMap<RoleRequest, Role>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<Permission, PermissionResponse>().IgnoreAllNonExisting();
        CreateMap<PermissionRequest, Permission>(MemberList.Source).IgnoreAllNonExisting();

        CreateMap<UserRoleDept, UserRoleDeptResponse>().IgnoreAllNonExisting();
        CreateMap<UserRoleDeptRequest, UserRoleDept>(MemberList.Source).IgnoreAllNonExisting();
    }
}
