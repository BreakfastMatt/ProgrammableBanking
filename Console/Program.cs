using Application.AuthenticationService;
using Domain.JsonSerialiser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Interfaces.ApplicationServices.AuthenticationService;
using Models.Interfaces.DomainServices.JsonSerialiser;

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
        services.AddSingleton<IJsonSerialiser, JsonSerialiser>();

        // Register Application Services
        services.AddScoped<IAuthenticationService, AuthenticationService>();
      })
      .Build();

    // Call the delegator service
    //var service = host.Services.GetRequiredService<IAuthenticationService>();
    //var response = service.TestAccessToken();
  }
}