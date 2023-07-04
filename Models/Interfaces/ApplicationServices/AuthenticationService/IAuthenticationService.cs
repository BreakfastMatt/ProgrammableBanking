namespace Models.Interfaces.ApplicationServices.AuthenticationService;

public interface IAuthenticationService
{
  /// <summary>
  /// Gets the access token for the Investec Programmable Banking API .
  /// </summary>
  /// <param name="uriBase">The uri for the target being hit (Investec API)</param>
  /// <returns>The access token</returns>
  public Task<string?> GetAccessTokenAsync(string uriBase = "https://openapi.investec.com");

  /// <summary>
  /// Gets the Authentication config (API Key & Client secret) for the Investec Programmable Banking API from the config.json file.
  /// </summary>
  /// <param name="fileName">The name of the config file to deserialise from</param>
  /// <returns>The Authentication config</returns>
  public IAuthenticationConfig GetClientAuthenticationConfig(string fileName = "config.json");
}