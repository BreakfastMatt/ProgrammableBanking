using Models.Models.ApplicationServices.AccountService;

namespace Models.Interfaces.ApplicationServices.AccountService;

public interface IAccountService
{
  /// <summary>
  /// Fetches a list of accounts to extract account ids
  /// </summary>
  /// <param name="accessToken">The Investec API Access Token</param>
  /// <param name="uriBase">The base URI (Investec API)</param>
  /// <returns>A list of Account Ids</returns>
  public Task<List<AccountIdentity>> GetAccountIdsAsync(string accessToken, string uriBase = "https://openapi.investec.com");
}