using Greenglobal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IPermissionRepository : IRepository<Permission, Guid>
    {
        IQueryable<Permission> GetListPermission();

        IQueryable<Permission> GetById(Guid id);

        IQueryable<Permission> GetByRoleFunction(Guid roleId, Guid functionId);

        IQueryable<Permission> GetByFunctionIds(List<Guid> functionIds);
    }
}
