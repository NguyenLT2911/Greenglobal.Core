using Greenglobal.Core.Entities;
using Greenglobal.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.Repositories
{
    public class RoleRepository : EfCoreRepository<CoreDbContext, Role, Guid>, IRoleRepository
    {
        public RoleRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public Task<bool> IsDupplication(string name, Guid applicationId)
        {
            return GetDbSetAsync().Result.AnyAsync(x => x.Name == name && x.ApplicationId == applicationId && (x.Status == 0 || x.Status == 1));
        }

        public int GetMaxSortOrder()
        {
            return GetDbSetAsync().Result.Where(x => x.Status == 0 || x.Status == 1).MaxAsync(x => (int?)x.SortOrder).Result ?? 0;
        }

        public IQueryable<Role> GetListRole(int? status)
        {
            return GetDbSetAsync().Result.WhereIf(!status.HasValue, x => x.Status == 0 || x.Status == 1)
                .WhereIf(status.HasValue && status.Value == -1, x => x.Status == -1)
                .AsNoTracking();
        }

        public IQueryable<Role> SearchKeyword(IQueryable<Role> query, string keyword)
        {
            return query.Where(x => EF.Functions.Unaccent(x.Name.ToLower().Trim()).Contains(EF.Functions.Unaccent(keyword.ToLower().Trim())));
        }
    }
}
