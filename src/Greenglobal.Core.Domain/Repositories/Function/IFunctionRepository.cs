using Greenglobal.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IFunctionRepository : IRepository<Function, Guid>
    {
        Task<bool> IsDupplicationName(string name);

        int GetMaxSortOrder(Guid? parentId);

        IQueryable<Function> GetListFunction(int? status);

        IQueryable<Function> SearchKeyword(IQueryable<Function> query, string keyword);

        IQueryable<Function> GetByParentId(Guid parentId);

        IQueryable<Function> GetById(Guid id);
    }
}
