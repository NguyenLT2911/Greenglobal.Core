using Greenglobal.Core.Entities;
using System;
using System.Linq;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IPermissionRepository : IRepository<Permission, Guid>
    {
        IQueryable<Permission> GetListPermission();

        IQueryable<Permission> GetById(Guid id);
    }
}
