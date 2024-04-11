using Greenglobal.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface ITitleRepository : IRepository<Title, Guid>
    {
        Task<bool> IsDupplicationName(string name);

        int GetMaxSortOrder();

        IQueryable<Title> GetListTitle(int? status);

        IQueryable<Title> SearchKeyword(IQueryable<Title> query, string keyword);
    }
}
