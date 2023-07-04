using Models.Interfaces.ApplicationServices.AuthenticationService;

namespace Models.Models.AuthenticationService;

public class AuthenticationConfig : IAuthenticationConfig
{
  public string ApiKey { get; set; }
  public string ClientId { get; set; }
  public string ClientSecret { get; set; }
}