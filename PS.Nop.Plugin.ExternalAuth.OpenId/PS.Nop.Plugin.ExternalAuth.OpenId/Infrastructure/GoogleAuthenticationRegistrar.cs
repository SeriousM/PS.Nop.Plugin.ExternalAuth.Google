using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Services.Authentication.External;
using System.Security.Claims;

namespace PS.Nop.Plugin.ExternalAuth.Google.Infrastructure
{

    public class GoogleAuthenticationRegistrar : IExternalAuthenticationRegistrar
    {
        public void Configure(AuthenticationBuilder builder)
        {
            builder.AddOpenIdConnect(options =>
            {
                var settings = EngineContext.Current.Resolve<GoogleExternalAuthSettings>();
                options.Authority = settings.Authority;
                options.ResponseType = settings.ResponseType;

                options.RequireHttpsMetadata = settings.RequiresHttps;

                var scopes = settings.Scopes?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
                foreach (var scope in scopes)
                {
                  options.Scope.Add(scope);
                }
                
                options.ClientId = settings.ClientKeyIdentifier;
                options.ClientSecret = settings.ClientSecret;
                options.SaveTokens = true;
            });
        }
    }
}