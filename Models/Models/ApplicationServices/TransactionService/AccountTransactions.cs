namespace Models.Models.ApplicationServices.TransactionService;

public class AccountTransactions
{
  /// <summary>
  /// Transaction description
  /// </summary>
  public string Description { get; set; }

  /// <summary>
  /// The transaction amount
  /// </summary>
  public decimal Amount { get; set; }

  /// <summary>
  /// Incoming cash indicator
  /// </summary>
  public bool IsCredit { get; set; }

  /// <summary>
  /// The date the transaction was made
  /// </summary>
  public DateTime TransactionDate { get; set; }

  /// <summary>
  /// The type of transaction
  /// </summary>
  public string TransactionType { get; set; }


  /// <summary>
  /// The account's running balance
  /// </summary>
  public decimal RunningBalance { get; set; }
}