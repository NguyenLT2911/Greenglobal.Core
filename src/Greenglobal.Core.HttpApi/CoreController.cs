using Greenglobal.Core.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Greenglobal.Core;

public abstract class CoreController : AbpControllerBase
{
    protected CoreController()
    {
        LocalizationResource = typeof(CoreResource);
    }
}
