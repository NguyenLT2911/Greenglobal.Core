using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class UserTitleRepository : EfCoreRepository<CoreDbContext, UserTitleDept, Guid>, IUserTitleRepository
    {
        public UserTitleRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public IQueryable<UserTitleDept> GetByUserId(Guid userId, bool haveIncludeMany)
        {
            var query = GetDbSetAsync().Result.Where(x => x.UserId == userId)
                .AsNoTracking();
            if (haveIncludeMany)
                query = query
                    .Include(x => x.Title)
                    .Include(x => x.Department).ThenInclude(x => x.Unit)
                    .AsNoTracking();
            return query;
        }

        public IQueryable<Guid> GetByDepartmentId(Guid departmentId)
        {
            return GetDbSetAsync().Result.Where(x => x.DepartmentId == departmentId && x.IsMain).AsNoTracking().Select(x => x.UserId);
        }
    }
}