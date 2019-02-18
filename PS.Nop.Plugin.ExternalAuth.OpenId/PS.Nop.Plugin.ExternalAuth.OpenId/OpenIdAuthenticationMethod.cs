using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Authentication.External;
using Nop.Services.Configuration;
using Nop.Services.Localization;

namespace PS.Nop.Plugin.ExternalAuth.OpenId
{
    public class OpenIdAuthenticationMethod : BasePlugin, IExternalAuthenticationMethod
    {
      private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public OpenIdAuthenticationMethod(ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper)
        {
            this._localizationService = localizationService;
            this._settingService = settingService;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/PSOpenIdAuthentication/Configure";
        }

        /// <summary>
        /// Gets a name of a view component for displaying plugin in public store
        /// </summary>
        /// <returns>View component name</returns>
        public string GetPublicViewComponentName()
        {
            return OpenIdExternalAuthConstants.ViewComponentName;
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        public override void Install()
        {
            //settings
            var defaultSettings = new OpenIdExternalAuthSettings
            {
              RequiresHttps = true, 
              Scopes = "openid profile email", 
              ResponseType = "id_token"
            };
            _settingService.SaveSetting(defaultSettings);

            //locales
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Authority", "Authentication Server *");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Authority.Hint", "The server address of the authentication server");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ResponseType", "Response Type *");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ResponseType.Hint", "The response type, usually \"id_token\"");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientId", "Client ID *");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientId.Hint", "Enter your client id");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientSecret", "Client Secret (optional)");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientSecret.Hint", "Enter your app secret here.");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.RequiresHttps", "HTTPS Required (recommended)");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.RequiresHttps.Hint", "Define if the communication with the Authentication Server is only allowed over https or not");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Scopes", "Scopes *");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Scopes.Hint", "The scopes required, usually \"openid profile email\"");
            //_localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Instructions", "<p>To configure authentication with OpenId, please follow these steps:<br /><br /></p><ol><li>Start by navigating to <a href=\"https://console.developers.google.com/projectselector/apis/library\" target=\"_blank\"> Google API Console</a>. Login using your gmail account.</li><li>On the API Manager and under the Library menu, choose to create a new project.</li><li>Provide Project name and agree to the terms of service</li><li>Next step is to create credentials to use with the API. This you can do under the menu Credentials. Here, press the button for Create Cedentials and then choose OAuth client ID</li><li>On the next page choose Web application under Application type and write a name in the Name input field. Go down to the section for Authorized redirect URIs, enter \"YourStoreUrl/signin-openid\" in that field (start with http or https). Copy or write down the Client ID and Client secret on the top of the page and then press Save.</li><li>Input the Client ID you copied in the last step into the App ID field below and the Client secret into the App secret field.</li></ol><p><br /><br /></p>");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Instructions", "<p>INTRO! Hint: You must restart the server instance after changes.</p>");

            base.Install();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<OpenIdExternalAuthSettings>();

            //locales
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Authority");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Authority.Hint");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ResponseType");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ResponseType.Hint");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientId");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientId.Hint");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientSecret");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientSecret.Hint");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.RequiresHttps");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.RequiresHttps.Hint");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Scopes");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Scopes.Hint");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Instructions");

            base.Uninstall();
        }
    }
}