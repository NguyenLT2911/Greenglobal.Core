using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Greenglobal.Core.Pages;

public class IndexModel : CorePageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
