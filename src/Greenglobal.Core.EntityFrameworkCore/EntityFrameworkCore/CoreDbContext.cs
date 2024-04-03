﻿using Greenglobal.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Greenglobal.Core.EntityFrameworkCore;

[ConnectionStringName(CoreDbProperties.ConnectionStringName)]
public class CoreDbContext : AbpDbContext<CoreDbContext>, ICoreDbContext
{
    public DbSet<Unit> Units { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRoleDept> UserRoleDepts { get; set; }

    public DbSet<Module> Modules { get; set; }

    public DbSet<Action> Actions { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public CoreDbContext(DbContextOptions<CoreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCore();
    }
}
