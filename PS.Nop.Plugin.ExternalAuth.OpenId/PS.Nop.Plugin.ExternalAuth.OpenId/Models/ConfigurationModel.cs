using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace PS.Nop.Plugin.ExternalAuth.OpenId.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("PS.Plugins.ExternalAuth.OpenId.ClientKeyIdentifier")]
        public string ClientId { get; set; }

        [NopResourceDisplayName("PS.Plugins.ExternalAuth.OpenId.ClientSecret")]
        public string ClientSecret { get; set; }
    }
}