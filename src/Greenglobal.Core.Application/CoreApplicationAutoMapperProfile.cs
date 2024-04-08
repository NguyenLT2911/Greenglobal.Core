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
        CreateMap<UnitRequest, Unit>(MemberList.Source);

        CreateMap<Department, DepartmentResponse>().IgnoreAllNonExisting();
        CreateMap<DepartmentRequest, Department>(MemberList.Source);

        CreateMap<Function, FunctionResponse>().IgnoreAllNonExisting();
        CreateMap<FunctionRequest, Function>(MemberList.Source);

        CreateMap<User, UserResponse>().IgnoreAllNonExisting();
        CreateMap<UserRequest, User>(MemberList.Source);

        CreateMap<Role, RoleResponse>().IgnoreAllNonExisting();
        CreateMap<RoleRequest, Role>(MemberList.Source);

        CreateMap<Permission, PermissionResponse>().IgnoreAllNonExisting();
        CreateMap<PermissionRequest, Permission>(MemberList.Source);

        CreateMap<UserRoleDept, UserRoleDeptResponse>().IgnoreAllNonExisting();
        CreateMap<UserRoleDeptRequest, UserRoleDept>(MemberList.Source);
    }
}
