namespace Models.Models.ApplicationServices.TransactionService;

/// <summary>
/// The transaction categories as configured in the config.json file
/// </summary>
public class AccountTransactionCategories
{
  public Dictionary<string, List<string>> AccountTransactions { get; set; }
}