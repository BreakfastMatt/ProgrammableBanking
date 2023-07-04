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

    // Act
    var accountTransactions = await transactionService.GetAccountTransactionsAsync(accountIds, accessToken, new DateTime(2023, 6, 26), new DateTime(2023, 7, 25));

    // Assert
    Assert.NotEmpty(accountTransactions);
  }

  [Fact]
  public void TestsTransactionCategoriesFromConfigJson()
  {
    // Arrange
    var jsonSerialiser = new JsonSerialiser();
    var transactionService = new TransactionService(jsonSerialiser);

    // Act
    var transactionCategories = transactionService.GetTransactionCategories();

    // Assert
    Assert.NotNull(transactionCategories);
  }

  [Fact]
  public async Task TestsTransactionCategoriesGroupingAsync()
  {
    // Arrange
    var jsonSerialiser = new JsonSerialiser();
    var authenticationService = new AuthenticationService(jsonSerialiser);
    var accountService = new AccountService(jsonSerialiser);
    var transactionService = new TransactionService(jsonSerialiser);
    var accessToken = await authenticationService.GetAccessTokenAsync();
    var accountIds = await accountService.GetAccountIdsAsync(accessToken);
    var accountTransactions = await transactionService.GetAccountTransactionsAsync(accountIds, accessToken, new DateTime(2023, 6, 26), new DateTime(2023, 7, 25));
    var transactionCategories = transactionService.GetTransactionCategories();

    // Act
    var groupedTransactions = transactionService.GroupAccountTransactions(accountTransactions, transactionCategories.AccountTransactions);

    // Assert
    Assert.NotNull(groupedTransactions);
  }
}