using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
    }
}
