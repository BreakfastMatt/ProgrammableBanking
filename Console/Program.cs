using Microsoft.Extensions.Hosting;

namespace ConsoleApp;

public class Program
{
  /// <summary>
  /// The main console application entry point
  /// </summary>
  public static async Task Main(string[] args)
  {
    // Set the synchronization context to null to disable context capturing
    SynchronizationContext.SetSynchronizationContext(null);

    // Create DI Container
    using var host = Host.CreateDefaultBuilder(args)
      .ConfigureServices(services =>
      {
        // Register Domain Services
        // TODO:

        // Register Application Services
        // TODO:
      })
      .Build();

    // Call the delegator service
    // TODO: add service here (var service = host.Services.GetRequiredService<IService>())
    // TODO: use service
  }
}