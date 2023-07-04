using Application.AuthenticationService;
using Domain.JsonSerialiser;

namespace ApplicationServiceTests;

public class AuthenticationServiceTests
{
  [Fact]
  public void TestsClientAuthenticationConfig()
  {
    // Arrange
    var jsonSerialiser = new JsonSerialiser();
    var authenticationService = new AuthenticationService(jsonSerialiser);

    // Act
    var authenticationConfig = authenticationService.GetClientAuthenticationConfig("config.json");

    // Assert
    Assert.NotNull(authenticationConfig);
  }

  [Fact]
  public async Task TestsAccessTokenAsync()
  {
    // Arrange
    var jsonSerialiser = new JsonSerialiser();
    var authenticationService = new AuthenticationService(jsonSerialiser);

    // Act
    var accessToken = await authenticationService.GetAccessTokenAsync("https://openapi.investec.com");

    // Assert
    Assert.NotNull(accessToken);
  }

  public async Task TestsAccessTokenSandboxAsync()
  {
    // Arrange
    var jsonSerialiser = new JsonSerialiser();
    var authenticationService = new AuthenticationService(jsonSerialiser);

    // Act
    //var accessToken = await authenticationService.GetAccessTokenAsync("https://openapi.investec.com");

    // Assert
    Assert.Fail("Not yet implemented");
  }
}