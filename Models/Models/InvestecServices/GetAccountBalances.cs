namespace Models.Models.InvestecServices;

/// <summary>
/// The generated response model for the GetAccountBalances Investec API call.
/// </summary>
public class GetAccountBalances : InvestecResponseType<AccountBalancesData>
{
}

public class AccountBalancesData
{
  public string AccountId { get; set; }
  public decimal CurrentBalance { get; set; }
  public decimal AvailableBalance { get; set; }
  public string Currency { get; set; }
}