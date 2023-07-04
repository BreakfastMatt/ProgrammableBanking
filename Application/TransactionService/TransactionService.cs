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
      .Where(accountTransaction => string.IsNullOrEmpty(accountTransaction?.Description) == false)
      .ToList();
    return accountTransactions;
  }
}