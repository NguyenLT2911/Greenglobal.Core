using Greenglobal.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IApplicationRepository : IRepository<Application, Guid>
    {
        Task<bool> IsDupplicationName(string name);

        Task<bool> IsDupplicationCode(string code);

        int GetMaxSortOrder();

        IQueryable<Application> GetListApplication(int? status);

        IQueryable<Application> SearchKeyword(IQueryable<Application> query, string keyword);

        IQueryable<Application> GetById(Guid id);
    }
}
