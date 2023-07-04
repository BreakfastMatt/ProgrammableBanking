using Models.Interfaces.ApplicationServices.TransactionService;
using Models.Interfaces.DomainServices.JsonSerialiser;
using Models.Models.ApplicationServices.AccountService;
using Models.Models.ApplicationServices.TransactionService;
using Models.Models.InvestecServices;

namespace Application.TransactionService;

public class TransactionService : ITransactionService
{
  private readonly IJsonSerialiser jsonSerialiser;
  public TransactionService(IJsonSerialiser jsonSerialiser)
  {
    this.jsonSerialiser = jsonSerialiser;
  }

  public async Task<List<AccountTransactions>> GetAccountTransactionsAsync(List<AccountIdentity> accountIds, string accessToken, DateTime fromDate, DateTime toDate, string uriBase = "https://openapi.investec.com")
  {
    // Make the API calls
    var client = new HttpClient();
    var getAccountTransactionsTasks = accountIds.Select(accountIdentity =>
    {
      // Set up the request
      var accountId = accountIdentity.AccountId;
      var url = $"{uriBase}/za/pb/v1/accounts/{accountId}/transactions?fromDate={fromDate}&toDate={toDate}";
      var request = new HttpRequestMessage(HttpMethod.Get, url);
      request.Headers.Add("Accept", "application/json");
      request.Headers.Add("Authorization", $"Bearer {accessToken}");
      return client.SendAsync(request);
    });
    var responseList = await Task.WhenAll(getAccountTransactionsTasks.ToList());

    // Extract the responses
    var responseContentList = await Task.WhenAll(responseList.Select(response => response.Content.ReadAsStringAsync()).ToList());
    var deserialisedResponseList = responseContentList
      .Select(responseContent => jsonSerialiser.DeserializeObject<GetAccountTransactions>(responseContent))
      .ToList();
    var accountTransactions = deserialisedResponseList
      .SelectMany(deserialisedResponse => deserialisedResponse.Data.Transactions
      .Select(transaction => new AccountTransactions
      {
        // Map out the account transactions
        Description = transaction?.Description ?? string.Empty,
        Amount = transaction?.Amount ?? 0m,
        IsCredit = transaction?.Type == "CREDIT",
        TransactionDate = transaction?.TransactionDate ?? DateTime.MinValue,
        TransactionType = transaction?.TransactionType ?? string.Empty,
        RunningBalance = transaction?.RunningBalance ?? 0m,
      }))
      .Where(accountTransaction => string.IsNullOrEmpty(accountTransaction?.Description) == false && accountTransaction.TransactionDate >= fromDate)
      .OrderBy(accountTransaction => accountTransaction.TransactionDate)
      .ToList();
    return accountTransactions;
  }

  public AccountTransactionCategories GetTransactionCategories(string fileName = "config.json")
  {
    // Read in the contents from the config.json file
    string basePath = AppContext.BaseDirectory;
    var configFilePath = Path.Combine(basePath, fileName);
    var configJson = File.ReadAllText(configFilePath) ?? string.Empty;

    // Deserialise the JSON to an object
    var deserialisedObject = jsonSerialiser.DeserializeObject<AccountTransactionCategories>(configJson) ?? new AccountTransactionCategories();
    return deserialisedObject;
  }

  public Dictionary<string, decimal> GroupAccountTransactions(List<AccountTransactions> accountTransactions, Dictionary<string, List<string>> transactionCategories)
  {
    var transactionGroups = new Dictionary<string, decimal>();
    foreach (var category in transactionCategories)
    {
      // Transaction category details
      var categoryName = category.Key;
      var categoryTransactionsFilter = category.Value;

      // Get the total value for the category
      var categoryTotal = accountTransactions
        .Where(transaction => categoryTransactionsFilter.Any(categoryDescription => transaction.Description.Contains(categoryDescription)))
        .Sum(categoryTransaction => categoryTransaction.IsCredit ? categoryTransaction.Amount : (0 - categoryTransaction.Amount));
      transactionGroups.Add(categoryName, categoryTotal);

      // Remove all categorised transactions
      accountTransactions.RemoveAll(transaction => categoryTransactionsFilter.Any(categoryDescription => transaction.Description.Contains(categoryDescription)));
    }

    // Add the uncategorised items
    var miscTotal = accountTransactions.Sum(transaction => transaction.IsCredit ? transaction.Amount : (0 - transaction.Amount));
    transactionGroups.Add("Misc", miscTotal);

    // Logging details (unused for now)
    var miscDescriptions = accountTransactions.Select(uncategorisedTransaction => new { Description = uncategorisedTransaction.Description, Amount = uncategorisedTransaction.Amount }).ToList();
    var openingBalance = transactionGroups.FirstOrDefault(transactionGroup => transactionGroup.Key == "Income").Value;
    var expenses = transactionGroups.Sum(category => category.Value < 0 ? Math.Abs(category.Value) : 0);
    var closingBalance = transactionGroups.Sum(category => category.Value); // Surplus or Defecit

    // Return the transactions
    return transactionGroups;
  }
}