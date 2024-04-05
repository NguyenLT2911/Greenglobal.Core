using Greenglobal.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IUnitRepository : IRepository<Unit, Guid>
    {
        Task<bool> IsDupplicationName(string name);

        int GetMaxSortOrder(Guid? parentId);

        IQueryable<Unit> GetListUnit(int? status);

        IQueryable<Unit> SearchKeyword(IQueryable<Unit> query, string keyword);

        IQueryable<Unit> GetByParentId(Guid parentId);
    }
}
