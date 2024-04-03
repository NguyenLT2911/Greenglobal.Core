using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class UnitRepository : EfCoreRepository<CoreDbContext, Unit, Guid>, IUnitRepository
    {
        public UnitRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}
