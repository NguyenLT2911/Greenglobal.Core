using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class UserRoleDeptRepository : EfCoreRepository<CoreDbContext, UserRoleDept, Guid>, IUserRoleDeptRepository
    {
        public UserRoleDeptRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}
