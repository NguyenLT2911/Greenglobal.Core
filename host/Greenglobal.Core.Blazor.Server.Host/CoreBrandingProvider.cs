using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Greenglobal.Core.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class CoreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Core";
}
