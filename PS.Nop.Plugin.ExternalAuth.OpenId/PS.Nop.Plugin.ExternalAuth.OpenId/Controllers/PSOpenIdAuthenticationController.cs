using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Authentication.External;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using PS.Nop.Plugin.ExternalAuth.OpenId.Models;

namespace PS.Nop.Plugin.ExternalAuth.OpenId.Controllers
{
    public class PSOpenIdAuthenticationController : BasePluginController
    {
      private readonly OpenIdExternalAuthSettings _openIdExternalAuthSettings;
        private readonly IExternalAuthenticationService _externalAuthenticationService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IGenericAttributeService _genericAttributeService;

        public PSOpenIdAuthenticationController(OpenIdExternalAuthSettings openIdExternalAuthSettings,
            IExternalAuthenticationService externalAuthenticationService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IGenericAttributeService genericAttributeService)
        {
            this._openIdExternalAuthSettings = openIdExternalAuthSettings;
            this._externalAuthenticationService = externalAuthenticationService;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._settingService = settingService;
            _genericAttributeService = genericAttributeService;
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageExternalAuthenticationMethods))
                return AccessDeniedView();

            var model = new ConfigurationModel
            {
                ClientId = _openIdExternalAuthSettings.ClientId,
                ClientSecret = _openIdExternalAuthSettings.ClientSecret,
                ResponseType = _openIdExternalAuthSettings.ResponseType,
                Scopes = _openIdExternalAuthSettings.Scopes,
                Authority = _openIdExternalAuthSettings.Authority,
                RequiresHttps = _openIdExternalAuthSettings.RequiresHttps
            };

            return View("~/Plugins/PS.ExternalAuth.OpenId/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAntiForgery]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageExternalAuthenticationMethods))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            //save settings
            _openIdExternalAuthSettings.ClientId = model.ClientId?.Trim();
            _openIdExternalAuthSettings.ClientSecret = model.ClientSecret?.Trim();
            _openIdExternalAuthSettings.ResponseType = model.ResponseType?.Trim();
            _openIdExternalAuthSettings.Scopes = model.Scopes?.Trim();
            _openIdExternalAuthSettings.Authority = model.Authority?.Trim();
            _openIdExternalAuthSettings.RequiresHttps = model.RequiresHttps;
            _settingService.SaveSetting(_openIdExternalAuthSettings);
            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        public IActionResult Login(string returnUrl)
        {
            if (!_externalAuthenticationService.ExternalAuthenticationMethodIsAvailable(OpenIdExternalAuthConstants.ProviderSystemName))
                throw new NopException("OpenId authentication module cannot be loaded");

            if (!_openIdExternalAuthSettings.IsValid())
                throw new NopException("OpenId authentication module not configured");

            //configure login callback action
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("LoginCallback", "PSOpenIdAuthentication", new { returnUrl = returnUrl })
            };

            return Challenge(authenticationProperties, OpenIdConnectDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> LoginCallback(string returnUrl)
        {
            //authenticate Facebook user
            var authenticateResult =  await this.HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded || !authenticateResult.Principal.Claims.Any())
                return RedirectToRoute("Login");

            //create external authentication parameters
            var authenticationParameters = new ExternalAuthenticationParameters
            {
                ProviderSystemName = OpenIdExternalAuthConstants.ProviderSystemName,
                Email = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email)?.Value,
                ExternalIdentifier = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value,
                ExternalDisplayIdentifier = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name)?.Value,
                Claims = authenticateResult.Principal.Claims.Select(claim => new ExternalAuthenticationClaim(claim.Type, claim.Value)).ToList()
            };

            var customer = _externalAuthenticationService.GetUserByExternalAuthenticationParameters(authenticationParameters);

            _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.LastNameAttribute, "aasdasda");

            //authenticate Nop user
            return _externalAuthenticationService.Authenticate(authenticationParameters, returnUrl);
        }
    }
}