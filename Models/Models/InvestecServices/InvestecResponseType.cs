namespace Models.Models.InvestecServices;

/// <summary>
/// The basic structure for the Investec API response
/// </summary>
/// <typeparam name="ServiceResponse">The specific Investec service response</typeparam>
public class InvestecResponseType<ServiceResponse>
{
  public ServiceResponse Data { get; set; }
  public Links Links { get; set; }
  public Meta Meta { get; set; }
}

public class Links
{
  public string Self { get; set; }
}

public class Meta
{
  public int TotalPages { get; set; }
}