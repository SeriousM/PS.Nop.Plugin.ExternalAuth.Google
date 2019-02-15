using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace PS.Nop.Plugin.ExternalAuth.OpenId.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("PS.Plugins.ExternalAuth.OpenId.Authority")]
        public string Authority { get; set; }

        [NopResourceDisplayName("PS.Plugins.ExternalAuth.OpenId.ResponseType")]
        public string ResponseType { get; set; }

        [NopResourceDisplayName("PS.Plugins.ExternalAuth.OpenId.ClientId")]
        public string ClientId { get; set; }

        [NopResourceDisplayName("PS.Plugins.ExternalAuth.OpenId.ClientSecret")]
        public string ClientSecret { get; set; }

        [NopResourceDisplayName("PS.Plugins.ExternalAuth.OpenId.RequiresHttps")]
        public bool RequiresHttps { get; set; }

        [NopResourceDisplayName("PS.Plugins.ExternalAuth.OpenId.Scopes")]
        public string Scopes { get; set; }
    }
}