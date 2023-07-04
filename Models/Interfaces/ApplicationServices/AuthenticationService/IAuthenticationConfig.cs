namespace Models.Interfaces.ApplicationServices.AuthenticationService;

/// <summary>
/// The API key & secret used to authenticate with Investec's Programmable Banking API.
/// </summary>
public interface IAuthenticationConfig
{
  /// <summary>
  /// Your personal Investec Programmable Banking API Key.
  /// </summary>
  public string ApiKey { get; set; }

  /// <summary>
  /// Your personal Investec Programmable Banking Client Id.
  /// </summary>
  public string ClientId { get; set; }

  /// <summary>
  /// Your personal Investec Programmable Banking secret used in conjunction with your API Key to authenticate.
  /// </summary>
  public string ClientSecret { get; set; }
}