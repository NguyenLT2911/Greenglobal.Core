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
    public class TitleRepository : EfCoreRepository<CoreDbContext, Title, Guid>, ITitleRepository
    {
        public TitleRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public Task<bool> IsDupplicationName(string name)
        {
            return GetDbSetAsync().Result.AnyAsync(x => x.Name == name);
        }

        public int GetMaxSortOrder()
        {
            return GetDbSetAsync().Result.Where(x => x.Status == 0 || x.Status == 1).MaxAsync(x => (int?)x.SortOrder).Result ?? 0;
        }

        public IQueryable<Title> GetListTitle(int? status)
        {
            return GetDbSetAsync().Result.WhereIf(!status.HasValue, x => x.Status == 0 || x.Status == 1)
                .WhereIf(status.HasValue && status.Value == -1, x => x.Status == -1)
                .AsNoTracking();
        }

        public IQueryable<Title> SearchKeyword(IQueryable<Title> query, string keyword)
        {
            return query.Where(x => EF.Functions.Unaccent(x.Name.ToLower().Trim()).Contains(EF.Functions.Unaccent(keyword.ToLower().Trim())));
        }
    }
}
