using Greenglobal.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        Task<bool> IsDupplicationName(string name);

        int GetMaxSortOrder();

        IQueryable<Role> GetListRole(int? status);

        IQueryable<Role> SearchKeyword(IQueryable<Role> query, string keyword);

        Task<bool> IsDupplicationDescription(string description);
    }
}