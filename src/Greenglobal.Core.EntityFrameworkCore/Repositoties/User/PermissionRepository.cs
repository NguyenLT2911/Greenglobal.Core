using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class PermissionRepository : EfCoreRepository<CoreDbContext, Permission, Guid>, IPermissionRepository
    {
        public PermissionRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public IQueryable<Permission> GetListPermission()
        {
            return GetDbSetAsync().Result.AsNoTracking();
        }

        public IQueryable<Permission> GetById(Guid id)
        {
            return GetDbSetAsync().Result.Where(x => x.Id == id).AsNoTracking();
        }

        public IQueryable<Permission> GetByRoleFunction(Guid roleId, Guid functionId)
        {
            return GetQueryableAsync().Result.Where(x => x.RoleId == roleId && x.FunctionId == functionId)
                .AsNoTracking();
        }

        public IQueryable<Permission> GetByFunctionIds(List<Guid> functionIds)
        {
            return GetQueryableAsync().Result.Where(x => functionIds.Contains(x.FunctionId))
                .Include(x => x.Role)
                .AsNoTracking();
        }
    }
}
