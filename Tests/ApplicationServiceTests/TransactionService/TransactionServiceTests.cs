using Application.AccountService;
using Application.AuthenticationService;
using Application.TransactionService;
using Domain.JsonSerialiser;

namespace ApplicationServiceTests;

public class TransactionServiceTests
{
  [Fact]
  public async Task TestsAccountTransactionsFetchAsync()
  {
    // Arrange
    var jsonSerialiser = new JsonSerialiser();
    var authenticationService = new AuthenticationService(jsonSerialiser);
    var accountService = new AccountService(jsonSerialiser);
    var transactionService = new TransactionService(jsonSerialiser);
    var accessToken = await authenticationService.GetAccessTokenAsync();
    var accountIds = await accountService.GetAccountIdsAsync(accessToken);
    var fromDate = new DateTime(2023, 6, 26);
    var toDate = new DateTime(2023, 7, 25);

    // Act
    var accountTransactions = await transactionService.GetAccountTransactionsAsync(accountIds, accessToken, fromDate, toDate);

    // Assert
    Assert.NotEmpty(accountTransactions);
  }
}