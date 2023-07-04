namespace Models.ApplicationServices.AccountService;

/// <summary>
/// The generated response model from the Investec API
/// </summary>
public class GetAccountList
{
  public AccountData Data { get; set; }
  public Links Links { get; set; }
  public Meta Meta { get; set; }
}

public class AccountData
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

public class Links
{
  public string Self { get; set; }
}

public class Meta
{
  public int TotalPages { get; set; }
}