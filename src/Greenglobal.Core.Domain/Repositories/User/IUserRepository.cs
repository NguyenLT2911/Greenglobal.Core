using Greenglobal.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {

        Task<bool> IsDupplicationUserName(string name);

        Task<bool> IsDupplicationEmail(string email);

        int GetMaxSortOrder();

        IQueryable<User> GetListUser(int? status);

        IQueryable<User> SearchKeyword(IQueryable<User> query, string fullName);

        IQueryable<User> GetById(Guid id);
    }
}
