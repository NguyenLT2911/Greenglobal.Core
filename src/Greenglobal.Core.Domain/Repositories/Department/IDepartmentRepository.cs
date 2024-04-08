using Greenglobal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
        Task<bool> IsDupplicationName(string name, Guid unitId);

        int GetMaxSortOrder(Guid? parentId);

        IQueryable<Department> GetListDepartment(int? status);

        IQueryable<Department> SearchKeyword(IQueryable<Department> query, string keyword);

        IQueryable<Department> GetByParentId(Guid parentId);

        IQueryable<Department> GetById(Guid id);

        IQueryable<Department> GetByUnitId(Guid unitId);

        IQueryable<Department> GetByUnitIds(List<Guid> unitIds);
    }
}
