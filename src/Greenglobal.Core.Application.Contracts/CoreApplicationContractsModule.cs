using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Greenglobal.Core;

[DependsOn(
    typeof(CoreDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class CoreApplicationContractsModule : AbpModule
{

}
