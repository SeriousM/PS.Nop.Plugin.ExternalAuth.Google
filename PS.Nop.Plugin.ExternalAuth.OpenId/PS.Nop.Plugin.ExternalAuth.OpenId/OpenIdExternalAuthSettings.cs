using Nop.Core.Configuration;

namespace PS.Nop.Plugin.ExternalAuth.OpenId
{
  public class OpenIdExternalAuthSettings : ISettings
  {
    /// <summary>
    /// The url of the authentication server
    /// </summary>
    public string Authority { get; set; }

    /// <summary>
    /// The response_type defined on the openId server eg. code, implicit
    /// </summary>
    public string ResponseType { get; set; }

    /// <summary>
    /// Gets or sets OAuth2 client identifier
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// Gets or sets OAuth2 client secret
    /// </summary>
    public string ClientSecret { get; set; }

    /// <summary>
    /// Enforces https communication (recommended)
    /// </summary>
    public bool RequiresHttps { get; set; }

    /// <summary>
    /// Scopes to request, separated with single space
    /// </summary>
    public string Scopes { get; set; }

    /// <summary>
    /// Name of the service
    /// </summary>
    public string ServiceName { get; set; }

    public bool IsValid()
    {
      return !string.IsNullOrWhiteSpace(Authority)
          && !string.IsNullOrWhiteSpace(ServiceName)
          && !string.IsNullOrWhiteSpace(ResponseType)
          && !string.IsNullOrWhiteSpace(ClientId)
          && !string.IsNullOrWhiteSpace(Scopes);
    }
  }
}
