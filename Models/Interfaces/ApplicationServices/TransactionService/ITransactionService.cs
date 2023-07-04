using Models.Models.ApplicationServices.AccountService;
using Models.Models.ApplicationServices.TransactionService;

namespace Models.Interfaces.ApplicationServices.TransactionService;

public interface ITransactionService
{
  /// <summary>
  /// Fetches a list of account transactions
  /// </summary>
  /// <param name="accountIds">A list of Account Ids</param>
  /// <param name="accessToken">The Investec API Access Token</param>
  /// <param name="fromDate">Fetch transactions from this date</param>
  /// <param name="toDate">Fetch transactions up until this date</param>
  /// <param name="uriBase">The base URI (Investec API)</param>
  /// <returns>A list of Account Transactions</returns>
  public Task<List<AccountTransactions>> GetAccountTransactionsAsync(List<AccountIdentity> accountIds, string accessToken, DateTime fromDate, DateTime toDate, string uriBase = "https://openapi.investec.com");
}