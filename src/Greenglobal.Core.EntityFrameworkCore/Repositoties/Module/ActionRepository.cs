using Greenglobal.Core.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class ActionRepository : EfCoreRepository<CoreDbContext, Entities.Action, Guid>, IActionRepository
    {
        public ActionRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}
