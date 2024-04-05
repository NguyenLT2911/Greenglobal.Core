using Greenglobal.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Greenglobal.Core.EntityFrameworkCore;

public static class CoreDbContextModelCreatingExtensions
{
    public static void ConfigureCore(
        this ModelBuilder builder,
            Action<CoreModelBuilderConfigurationOptions> optionsAction = null)
    {
        Check.NotNull(builder, nameof(builder));

        var options = new CoreModelBuilderConfigurationOptions(
                CoreDbProperties.DbTablePrefix,
                CoreDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

        builder.Entity<Unit>(b =>
        {
            b.ToTable(options.TablePrefix + "Units", CoreDbProperties.DbSchemaAuth);
            b.ConfigureByConvention();
        });

        builder.Entity<Department>(b =>
        {
            b.ToTable(options.TablePrefix + "Departments", CoreDbProperties.DbSchemaAuth);
            b.ConfigureByConvention();
        });

        builder.Entity<Role>(b =>
        {
            b.ToTable(options.TablePrefix + "Roles", CoreDbProperties.DbSchemaAuth);
            b.ConfigureByConvention();
        });

        builder.Entity<User>(b =>
        {
            b.ToTable(options.TablePrefix + "Users", CoreDbProperties.DbSchemaAuth);
            b.ConfigureByConvention();
        });

        builder.Entity<UserRoleDept>(b =>
        {
            b.ToTable(options.TablePrefix + "UserRoleDepts", CoreDbProperties.DbSchemaAuth);
            b.ConfigureByConvention();
        });

        builder.Entity<Function>(b =>
        {
            b.ToTable(options.TablePrefix + "Functions", CoreDbProperties.DbSchemaAuth);
            b.ConfigureByConvention();
        });

        builder.Entity<Permission>(b =>
        {
            b.ToTable(options.TablePrefix + "Permissions", CoreDbProperties.DbSchemaAuth);
            b.ConfigureByConvention();
        });
    }
}
