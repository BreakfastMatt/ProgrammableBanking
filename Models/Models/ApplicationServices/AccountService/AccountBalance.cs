namespace Models.Models.ApplicationServices.AccountService;

/// <summary>
/// Used to map the GetAccountBalances response to
/// </summary>
public class AccountBalance
{
  public string AccountId { get; set; }
  public decimal CurrentBalance { get; set; }
}