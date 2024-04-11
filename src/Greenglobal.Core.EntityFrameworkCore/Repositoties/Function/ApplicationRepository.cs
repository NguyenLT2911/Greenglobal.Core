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
    public class ApplicationRepository : EfCoreRepository<CoreDbContext, Application, Guid>, IApplicationRepository
    {
        public ApplicationRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public Task<bool> IsDupplicationName(string name)
        {
            return GetDbSetAsync().Result.AnyAsync(x => x.Name == name && (x.Status == 0 || x.Status == 1));
        }

        public Task<bool> IsDupplicationCode(string code)
        {
            return GetDbSetAsync().Result.AnyAsync(x => x.Code == code && (x.Status == 0 || x.Status == 1));
        }

        public int GetMaxSortOrder()
        {
            return GetDbSetAsync().Result.Where(x => x.Status == 0 || x.Status == 1).MaxAsync(x => (int?)x.SortOrder).Result ?? 0;
        }

        public IQueryable<Application> GetListApplication(int? status)
        {
            return GetDbSetAsync().Result.WhereIf(!status.HasValue, x => (x.Status == 0 || x.Status == 1))
                .WhereIf(status.HasValue && status.Value == -1, x => x.Status == -1)
                .AsNoTracking();
        }

        public IQueryable<Application> SearchKeyword(IQueryable<Application> query, string keyword)
        {
            return query.Where(x => EF.Functions.Unaccent(x.Name.ToLower().Trim()).Contains(EF.Functions.Unaccent(keyword.ToLower().Trim()))
            || EF.Functions.Unaccent(x.Code.ToLower().Trim()).Contains(EF.Functions.Unaccent(keyword.ToLower().Trim())));
        }

        public IQueryable<Application> GetById(Guid id)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1) && x.Id == id).AsNoTracking();
        }
    }
}
