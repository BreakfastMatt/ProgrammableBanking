using Application.AccountService;
using Application.AuthenticationService;
using Domain.JsonSerialiser;

namespace ApplicationServiceTests;

public class AccountServiceTests
{
  [Fact]
  public async Task TestsAccountIdsFetchAsync()
  {
    // Arrange
    var jsonSerialiser = new JsonSerialiser();
    var authenticationService = new AuthenticationService(jsonSerialiser);
    var accessToken = await authenticationService.GetAccessTokenAsync();
    var accountService = new AccountService(jsonSerialiser);

    // Act
    var accountIdList = await accountService.GetAccountIdsAsync(accessToken);

    // Assert
    Assert.NotEmpty(accountIdList);
  }
}