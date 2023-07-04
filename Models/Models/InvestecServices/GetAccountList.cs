namespace Models.Models.InvestecServices;

/// <summary>
/// The generated response model from the Investec API for the GetAccountList call
/// </summary>
public class GetAccountList : InvestecResponseType<AccountListData>
{
}

public class AccountListData
{
  public List<AccountType> Accounts { get; set; }
}

public class AccountType
{
  public string AccountId { get; set; }
  public string AccountNumber { get; set; }
  public string AccountName { get; set; }
  public string ReferenceName { get; set; }
  public string ProductName { get; set; }
  public bool KycCompliant { get; set; }
  public long ProfileId { get; set; }
  public string ProfileName { get; set; }
}