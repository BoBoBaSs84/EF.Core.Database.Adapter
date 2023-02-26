using DA.BaseTests;
using DA.Infrastructure.Installer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace DA.InfrastructureTests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest - Not relevant here.")]
[SuppressMessage("Style", "IDE0053", Justification = "UnitTest - Not relevant here.")]
public class InfrastructureBaseTests : BaseTestUnit
{
	private static IHost testHost = default!;

	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context) =>
		testHost = InitializeHost(context);

	private static IHost InitializeHost(TestContext context)
	{
		context.WriteLine($"{nameof(InitializeHost)} ...");

		IHostBuilder hostBuilder = Host.CreateDefaultBuilder(new[] { string.Empty })
			.ConfigureServices((hostContext, services) =>
			{
				services.AddInfrastructureServices();
			});

		return hostBuilder.Start();
	}

	/// <summary>
	/// Should return the requested service if it was registered within the service collection.
	/// </summary>
	/// <typeparam name="TService">The type of service we are operating on.</typeparam>
	/// <returns>The requested service.</returns>
	public static TService GetRequiredService<TService>() =>
		(TService)testHost.Services.GetRequiredService(typeof(TService));
}
