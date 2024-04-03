using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Greenglobal.Core.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(CoreBlazorModule)
    )]
public class CoreBlazorServerModule : AbpModule
{

}
