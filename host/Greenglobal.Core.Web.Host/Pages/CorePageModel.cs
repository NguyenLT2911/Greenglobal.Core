using Greenglobal.Core.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Greenglobal.Core.Pages;

public abstract class CorePageModel : AbpPageModel
{
    protected CorePageModel()
    {
        LocalizationResourceType = typeof(CoreResource);
    }
}
