using Greenglobal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IFunctionRepository : IRepository<Function, Guid>
    {
        Task<bool> IsDupplicationName(string name, Guid? parentId, Guid applicationId);

        Task<bool> IsDupplicationCode(string code, Guid? parentId, Guid applicationId);

        int GetMaxSortOrder(Guid? parentId);

        IQueryable<Function> GetListFunction(int? status, bool isModule, Guid? parentId);

        IQueryable<Function> SearchKeyword(IQueryable<Function> query, string keyword);

        IQueryable<Function> GetByParentId(Guid parentId);

        IQueryable<Function> GetByParentIds(List<Guid> parentIds);

        IQueryable<Function> GetById(Guid id);
    }
}
