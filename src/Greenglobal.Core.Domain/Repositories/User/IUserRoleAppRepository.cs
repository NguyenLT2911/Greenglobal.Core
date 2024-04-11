using Greenglobal.Core.Entities;
using System;
using System.Linq;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IUserRoleAppRepository : IRepository<UserRoleApp, Guid>
    {
        IQueryable<UserRoleApp> GetByUserId(Guid userId, bool haveIncludeMany = true);
    }
}
