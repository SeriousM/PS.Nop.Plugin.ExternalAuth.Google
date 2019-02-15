using System.Linq;
using System.Security.Claims;
using Nop.Core.Domain.Customers;
using Nop.Services.Authentication.External;
using Nop.Services.Common;
using Nop.Services.Events;

namespace PS.Nop.Plugin.ExternalAuth.OpenId.Infrastructure.Cache
{
    /// <summary>
    /// OpenIdConnect authentication event consumer (used for saving customer fields on registration)
    /// </summary>
    public partial class OpenIdAuthenticationEventConsumer : IConsumer<CustomerAutoRegisteredByExternalMethodEvent>
    {
      private readonly IGenericAttributeService _genericAttributeService;

      public OpenIdAuthenticationEventConsumer(IGenericAttributeService genericAttributeService)
        {
            this._genericAttributeService = genericAttributeService;
        }

      public void HandleEvent(CustomerAutoRegisteredByExternalMethodEvent eventMessage)
        {
            if (eventMessage?.Customer == null || eventMessage.AuthenticationParameters == null)
                return;

            //handle event only for this authentication method
            if (!eventMessage.AuthenticationParameters.ProviderSystemName.Equals(OpenIdExternalAuthConstants.ProviderSystemName))
                return;

            //store some of the customer fields
            var firstName = eventMessage.AuthenticationParameters.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value;
            if (!string.IsNullOrEmpty(firstName))
                _genericAttributeService.SaveAttribute(eventMessage.Customer, NopCustomerDefaults.FirstNameAttribute, firstName);

            var lastName = eventMessage.AuthenticationParameters.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value;
            if (!string.IsNullOrEmpty(lastName))
                _genericAttributeService.SaveAttribute(eventMessage.Customer, NopCustomerDefaults.LastNameAttribute, lastName);
        }
    }
}