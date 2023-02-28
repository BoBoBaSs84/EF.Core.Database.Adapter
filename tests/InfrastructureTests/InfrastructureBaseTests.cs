using BaseTests;
using Infrastructure.Extensions;
using Infrastructure.Installer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Domain.Constants.Environment;

namespace InfrastructureTests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest - Not relevant here.")]
[SuppressMessage("Style", "IDE0053", Justification = "UnitTest - Not relevant here.")]
public class InfrastructureBaseTests : BaseTestUnit
{
	private static IHost testHost = default!;

	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context) =>
		testHost = InitializeHost(context);

	/// <summary>
	/// Should return the requested service if it was registered within the service collection.
	/// </summary>
	/// <typeparam name="TService">The type of service we are operating on.</typeparam>
	/// <returns>The requested service.</returns>
	public static TService GetRequiredService<TService>() =>
		(TService)testHost.Services.GetRequiredService(typeof(TService));

	private static IHost InitializeHost(TestContext context)
	{
		context.WriteLine($"{nameof(InitializeHost)} ...");

		IHostBuilder hostBuilder = Host.CreateDefaultBuilder(new[] { string.Empty })
			.ConfigureAppSettings(Test)
			.ConfigureServices((hostContext, services) =>
			{
				services.AddInfrastructureServices(hostContext.Configuration, hostContext.HostingEnvironment);
			});

		return hostBuilder.Start();
	}
}
