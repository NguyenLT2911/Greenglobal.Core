﻿using Greenglobal.Core.Entities;
using System;
using Volo.Abp.Domain.Repositories;

namespace Greenglobal.Core.Repositories
{
    public interface IPermissionRepository : IRepository<Permission, Guid>
    {
    }
}
