using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace PS.Nop.Plugin.ExternalAuth.OpenId.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("PS.Plugins.ExternalAuth.Google.ClientKeyIdentifier")]
        public string ClientId { get; set; }

        [NopResourceDisplayName("PS.Plugins.ExternalAuth.Google.ClientSecret")]
        public string ClientSecret { get; set; }
    }
}