using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class PermissionRepository : EfCoreRepository<CoreDbContext, Permission, Guid>, IPermissionRepository
    {
        public PermissionRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}
