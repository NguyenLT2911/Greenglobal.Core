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
    public class DepartmentRepository : EfCoreRepository<CoreDbContext, Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider) { }

        public Task<bool> IsDupplicationName(string name)
        {
            return GetDbSetAsync().Result.AnyAsync(x => x.Name == name);
        }

        public int GetMaxSortOrder(Guid? parentId)
        {
            return GetDbSetAsync().Result.Where(x => x.ParentId == parentId && (x.Status == 0 || x.Status == 1)).MaxAsync(x => (int?)x.SortOrder).Result ?? 0;
        }

        public IQueryable<Department> GetListDepartment(int? status)
        {
            return GetDbSetAsync().Result.WhereIf(!status.HasValue, x => x.Status == 0 || x.Status == 1)
                .WhereIf(status.HasValue && status.Value == -1, x => x.Status == -1)
                .AsNoTracking();
        }

        public IQueryable<Department> SearchKeyword(IQueryable<Department> query, string keyword)
        {
            return query.Where(x => EF.Functions.Unaccent(x.Name.ToLower().Trim()).Contains(EF.Functions.Unaccent(keyword.ToLower().Trim())));
        }

        public IQueryable<Department> GetByParentId(Guid parentId)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1)
            && x.ParentId.HasValue && x.ParentId.Value == parentId).AsNoTracking();
        }

        public IQueryable<Department> GetById(Guid id)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1) && x.Id == id)
                .Include(x => x.Unit).AsNoTracking();
        }

        public IQueryable<Department> GetByUnitId(Guid unitId)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1) && x.UnitId == unitId).AsNoTracking();
        }

        public IQueryable<Department> GetByUnitIds(List<Guid> unitIds)
        {
            return GetDbSetAsync().Result.Where(x => (x.Status == 0 || x.Status == 1) && unitIds.Contains(x.UnitId)).AsNoTracking();
        }
    }
}
