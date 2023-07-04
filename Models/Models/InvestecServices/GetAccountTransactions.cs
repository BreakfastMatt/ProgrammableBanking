namespace Models.Models.InvestecServices;

/// <summary>
/// The generated response model for the GetAccountTransactions Investec API call.
/// </summary>
public class GetAccountTransactions : InvestecResponseType<AccountTranasctionsData>
{
}

public class AccountTranasctionsData
{
  public List<AccountTransactionDataDetail> Transactions { get; set; }
}

public class AccountTransactionDataDetail
{
  public string AccountId { get; set; }
  public string Type { get; set; }
  public string TransactionType { get; set; }
  public string Status { get; set; }
  public string Description { get; set; }
  public string CardNumber { get; set; }
  public int PostedOrder { get; set; }
  public DateTime PostingDate { get; set; }
  public DateTime ValueDate { get; set; }
  public DateTime ActionDate { get; set; }
  public DateTime TransactionDate { get; set; }
  public decimal Amount { get; set; }
  public decimal RunningBalance { get; set; }
}