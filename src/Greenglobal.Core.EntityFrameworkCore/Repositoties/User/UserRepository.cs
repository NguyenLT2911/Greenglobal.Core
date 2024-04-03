using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class UserRepository : EfCoreRepository<CoreDbContext, User, Guid>, IUserRepository
    {
        public UserRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}
