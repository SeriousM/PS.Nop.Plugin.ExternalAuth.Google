using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace PS.Nop.Plugin.ExternalAuth.OpenId.Components
{
    [ViewComponent(Name = GoogleExternalAuthConstants.ViewComponentName)]
    public class GoogleAuthenticationViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Plugins/PS.ExternalAuth.Google/Views/PublicInfo.cshtml");
        }
    }
}