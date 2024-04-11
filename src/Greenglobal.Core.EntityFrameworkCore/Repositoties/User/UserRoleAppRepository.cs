using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class UserRoleAppRepository : EfCoreRepository<CoreDbContext, UserRoleApp, Guid>, IUserRoleAppRepository
    {
        public UserRoleAppRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public IQueryable<UserRoleApp> GetByUserId(Guid userId, bool haveIncludeMany)
        {
            var query = GetDbSetAsync().Result.Where(x => x.UserId == userId)
                .AsNoTracking();
            if (haveIncludeMany)
                query = query
                    .Include(x => x.Role)
                    .Include(x => x.Application)
                    .AsNoTracking();
            return query;
        }
    }
}
