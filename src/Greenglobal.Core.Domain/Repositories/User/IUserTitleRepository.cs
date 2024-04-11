using Greenglobal.Core.Entities;
using System;
using System.Linq;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IUserTitleRepository : IRepository<UserTitleDept, Guid>
    {
        IQueryable<UserTitleDept> GetByUserId(Guid userId, bool haveIncludeMany = true);

        IQueryable<Guid> GetByDepartmentId(Guid departmentId);
    }
}