using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class UserRoleDeptRepository : EfCoreRepository<CoreDbContext, UserRoleDept, Guid>, IUserRoleDeptRepository
    {
        public UserRoleDeptRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public IQueryable<UserRoleDept> GetByUserId(Guid userId, bool haveIncludeMany)
        {
            var query = GetDbSetAsync().Result.Where(x => x.UserId == userId)
                .AsNoTracking();
            if (haveIncludeMany)
                query = query
                    .Include(x => x.Role)
                    .Include(x => x.Department).ThenInclude(x => x.Unit)
                    .AsNoTracking();
            return query;
        }

        public IQueryable<Guid> GetByDepartmentId(Guid departmentId)
        {
            return GetDbSetAsync().Result.Where(x => x.DepartmentId == departmentId).AsNoTracking().Select(x => x.UserId);
        }
    }
}
