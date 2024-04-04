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
    public class UserRepository : EfCoreRepository<CoreDbContext, User, Guid>, IUserRepository
    {
        public UserRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public Task<bool> IsDupplicationUserName(string userName)
        {
            return GetDbSetAsync().Result.AnyAsync(x => x.UserName == userName);
        }

        public Task<bool> IsDupplicationEmail(string email)
        {
            return GetDbSetAsync().Result.AnyAsync(x => x.Email == email);
        }

        public int GetMaxSortOrder()
        {
            return GetDbSetAsync().Result.Where(x => x.Status == 0 || x.Status == 1).MaxAsync(x => (int?)x.SortOrder).Result ?? 0;
        }

        public IQueryable<User> GetListUser(int? status)
        {
            return GetDbSetAsync().Result.WhereIf(!status.HasValue, x => x.Status == 0 || x.Status == 1)
                .WhereIf(status.HasValue && status.Value == -1, x => x.Status == -1)
                .AsNoTracking();
        }

        public IQueryable<User> SearchKeyword(IQueryable<User> query, string keyword)
        {
            return query.Where(x => EF.Functions.Unaccent(x.FullName.ToLower().Trim()).Contains(EF.Functions.Unaccent(keyword.ToLower().Trim()))
            || EF.Functions.Unaccent(x.UserName.ToLower().Trim()).Contains(EF.Functions.Unaccent(keyword.ToLower().Trim())));
        }

        public IQueryable<User> GetById(Guid id)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1) && x.Id == id).AsNoTracking();
        }
    }
}
