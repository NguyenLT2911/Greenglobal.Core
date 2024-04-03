using Volo.Abp.Bundling;

namespace Greenglobal.Core.Blazor.Host;

public class CoreBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
