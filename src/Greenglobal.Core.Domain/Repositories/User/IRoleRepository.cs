using Greenglobal.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        Task<bool> IsDupplication(string name, Guid applicationId);

        int GetMaxSortOrder();

        IQueryable<Role> GetListRole(int? status);

        IQueryable<Role> SearchKeyword(IQueryable<Role> query, string keyword);
    }
}