using System.Text;
using Models.Interfaces.ApplicationServices.AuthenticationService;
using Models.Interfaces.DomainServices.JsonSerialiser;
using Models.Models.ApplicationServices.AuthenticationService;

namespace Application.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
  private readonly IJsonSerialiser jsonSerialiser;
  public AuthenticationService(IJsonSerialiser jsonSerialiser)
  {
    this.jsonSerialiser = jsonSerialiser;
  }

  public async Task<string?> GetAccessTokenAsync(string uriBase = "https://openapi.investec.com")
  {
    // Gets the authentication details 
    var authenticationConfig = GetClientAuthenticationConfig();
    var authHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{authenticationConfig.ClientId}:{authenticationConfig.ClientSecret}"));

    // Set up the request
    var request = new HttpRequestMessage(HttpMethod.Post, $"{uriBase}/identity/v2/oauth2/token");
    request.Headers.Add("Accept", "application/json");
    request.Headers.Add("x-api-key", authenticationConfig.ApiKey);
    request.Headers.Add("Authorization", $"Basic {authHeader}");
    request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() { new("grant_type", "client_credentials") });

    // Make the API call
    var httpClient = new HttpClient();
    var response = await httpClient.SendAsync(request);
    var responseContent = await response.Content.ReadAsStringAsync();

    // Extract the Access Token
    var accessToken = jsonSerialiser.ParseDocumentProperty(responseContent, "access_token");
    return accessToken;
  }

  public IAuthenticationConfig GetClientAuthenticationConfig(string fileName = "config.json")
  {
    // Read in the contents from the config.json file
    string basePath = AppContext.BaseDirectory;
    var configFilePath = Path.Combine(basePath, fileName);
    var configJson = File.ReadAllText(configFilePath) ?? string.Empty;

    // Deserialise the JSON to an object
    var deserialisedObject = jsonSerialiser.DeserializeObject<AuthenticationConfig>(configJson) ?? new AuthenticationConfig();
    return deserialisedObject;
  }
}