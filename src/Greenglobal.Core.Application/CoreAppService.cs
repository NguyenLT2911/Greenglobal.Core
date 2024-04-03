using Greenglobal.Core.Localization;
using Volo.Abp.Application.Services;

namespace Greenglobal.Core;

public abstract class CoreAppService : ApplicationService
{
    protected CoreAppService()
    {
        LocalizationResource = typeof(CoreResource);
        ObjectMapperContext = typeof(CoreApplicationModule);
    }
}
