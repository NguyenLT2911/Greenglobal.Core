using Greenglobal.Core.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Greenglobal.Core.Blazor.Server.Host;

public abstract class CoreComponentBase : AbpComponentBase
{
    protected CoreComponentBase()
    {
        LocalizationResource = typeof(CoreResource);
    }
}
