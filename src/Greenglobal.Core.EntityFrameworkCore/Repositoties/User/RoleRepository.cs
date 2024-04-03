using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class RoleRepository : EfCoreRepository<CoreDbContext, Role, Guid>, IRoleRepository
    {
        public RoleRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}
