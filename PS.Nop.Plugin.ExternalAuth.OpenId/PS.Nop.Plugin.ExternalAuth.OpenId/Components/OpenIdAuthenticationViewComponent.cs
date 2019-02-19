using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace PS.Nop.Plugin.ExternalAuth.OpenId.Components
{
  [ViewComponent(Name = OpenIdExternalAuthConstants.ViewComponentName)]
  public class OpenIdAuthenticationViewComponent : NopViewComponent
  {
    public IViewComponentResult Invoke()
    {
      return View("~/Plugins/PS.ExternalAuth.OpenId/Views/PublicInfo.cshtml");
    }
  }
}
