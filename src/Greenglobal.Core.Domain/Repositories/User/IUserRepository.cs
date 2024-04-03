﻿using Greenglobal.Core.Entities;
using System;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
