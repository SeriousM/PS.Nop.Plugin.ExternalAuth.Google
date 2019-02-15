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
            _settingService.SaveSetting(new OpenIdExternalAuthSettings());

            //locales
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientKeyIdentifier", "App ID/API Key");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientKeyIdentifier.Hint", "Enter your app ID/API key here. You can find it on your FaceBook application page.");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientSecret", "App Secret");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientSecret.Hint", "Enter your app secret here. You can find it on your FaceBook application page.");
            _localizationService.AddOrUpdatePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Instructions", "<p>To configure authentication with OpenId, please follow these steps:<br /><br /></p><ol><li>Start by navigating to <a href=\"https://console.developers.google.com/projectselector/apis/library\" target=\"_blank\"> Google API Console</a>. Login using your gmail account.</li><li>On the API Manager and under the Library menu, choose to create a new project.</li><li>Provide Project name and agree to the terms of service</li><li>Next step is to create credentials to use with the API. This you can do under the menu Credentials. Here, press the button for Create Cedentials and then choose OAuth client ID</li><li>On the next page choose Web application under Application type and write a name in the Name input field. Go down to the section for Authorized redirect URIs, enter \"YourStoreUrl/signin-google\" in that field (start with http or https). Copy or write down the Client ID and Client secret on the top of the page and then press Save.</li><li>Input the Client ID you copied in the last step into the App ID field below and the Client secret into the App secret field.</li></ol><p><br /><br /></p>");

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
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientKeyIdentifier");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientKeyIdentifier.Hint");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientSecret");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.ClientSecret.Hint");
            _localizationService.DeletePluginLocaleResource("PS.Plugins.ExternalAuth.OpenId.Instructions");

            base.Uninstall();
        }
    }
}