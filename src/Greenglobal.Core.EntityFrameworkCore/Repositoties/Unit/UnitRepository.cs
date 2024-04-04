using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class UnitRepository : EfCoreRepository<CoreDbContext, Unit, Guid>, IUnitRepository
    {
        public UnitRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public Task<bool> IsDupplicationName(string name)
        {
            return GetDbSetAsync().Result.AnyAsync(x => x.Name == name);
        }

        public int GetMaxSortOrder(Guid? parentId)
        {
            return GetDbSetAsync().Result.Where(x => x.ParentId == parentId && (x.Status == 0 || x.Status == 1)).MaxAsync(x => (int?)x.SortOrder).Result ?? 0;
        }

        public IQueryable<Unit> GetListUnit(int? status)
        {
            return GetDbSetAsync().Result.WhereIf(!status.HasValue, x => x.Status == 0 || x.Status == 1)
                .WhereIf(status.HasValue && status.Value == -1, x => x.Status == -1)
                .AsNoTracking();
        }

        public IQueryable<Unit> SearchKeyword(IQueryable<Unit> query, string keyword)
        {
            return query.Where(x => EF.Functions.Unaccent(x.Name.ToLower().Trim()).Contains(EF.Functions.Unaccent(keyword.ToLower().Trim())));
        }

        public IQueryable<Unit> GetByParentId(Guid parentId)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1)
            && x.ParentId.HasValue && x.ParentId.Value == parentId).AsNoTracking();
        }
    }
}
