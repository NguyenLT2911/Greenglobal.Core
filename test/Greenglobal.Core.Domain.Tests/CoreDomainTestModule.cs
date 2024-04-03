using Volo.Abp.Modularity;

namespace Greenglobal.Core;

[DependsOn(
    typeof(CoreDomainModule),
    typeof(CoreTestBaseModule)
)]
public class CoreDomainTestModule : AbpModule
{

}
