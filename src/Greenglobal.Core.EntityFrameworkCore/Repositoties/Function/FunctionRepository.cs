using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class FunctionRepository : EfCoreRepository<CoreDbContext, Function, Guid>, IFunctionRepository
    {
        public FunctionRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public Task<bool> IsDupplicationName(string name)
        {
            return GetDbSetAsync().Result.AnyAsync(x => x.Name == name);
        }

        public int GetMaxSortOrder(Guid? parentId)
        {
            return GetDbSetAsync().Result.Where(x => x.ParentId == parentId && (x.Status == 0 || x.Status == 1)).MaxAsync(x => (int?)x.SortOrder).Result ?? 0;
        }

        public IQueryable<Function> GetListFunction(int? status, bool isModule, Guid? parentId)
        {
            return GetDbSetAsync().Result.WhereIf(!status.HasValue, x => (x.Status == 0 || x.Status == 1) 
                && x.IsModule == isModule && x.ParentId == parentId)
                .WhereIf(status.HasValue && status.Value == -1, x => x.Status == -1 && x.IsModule == isModule 
                && x.ParentId == parentId)
                .AsNoTracking();
        }

        public IQueryable<Function> SearchKeyword(IQueryable<Function> query, string keyword)
        {
            return query.Where(x => EF.Functions.Unaccent(x.Name.ToLower().Trim()).Contains(EF.Functions.Unaccent(keyword.ToLower().Trim())));
        }

        public IQueryable<Function> GetByParentId(Guid parentId)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1)
            && x.ParentId.HasValue && x.ParentId.Value == parentId).AsNoTracking();
        }

        public IQueryable<Function> GetByParentIds(List<Guid> parentIds)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1)
            && x.ParentId.HasValue && parentIds.Contains(x.ParentId.Value)).AsNoTracking();
        }

        public IQueryable<Function> GetById(Guid id)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1) && x.Id == id).AsNoTracking();
        }
    }
}
