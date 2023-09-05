using Application.Installer;
using Application.Interfaces.Presentation.Services;

using BaseTests.Services;

using Domain.Constants;

using Infrastructure.Extensions;
using Infrastructure.Installer;
using Infrastructure.Persistence;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BaseTests;

[TestClass]
public abstract class TestBase
{
	private static IHost s_host = default!;

	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context)
	{
		if (s_host is not null)
			return;

		s_host = InitializeHost();
		RepositoryContext dbContext = GetRequiredService<RepositoryContext>();
		dbContext.Database.EnsureCreated();
	}

	[AssemblyCleanup]
	public static void AssemblyCleanup()
	{
		RepositoryContext dbContext = GetRequiredService<RepositoryContext>();
		dbContext.Database.EnsureDeleted();
	}

	/// <summary>
	/// Returns the requested service if it was registered within the service collection.
	/// </summary>
	/// <typeparam name="TService">The type of service we are operating on.</typeparam>
	/// <returns>The requested service.</returns>
	public static TService GetRequiredService<TService>() =>
		(TService)s_host.Services.GetRequiredService(typeof(TService));

	private static IHost InitializeHost()
	{
		string env = DomainConstants.Environment.Testing;

		IHostBuilder hostBuilder = Host.CreateDefaultBuilder()
			.ConfigureAppSettings(env)
			.UseEnvironment(env)
			.ConfigureServices((hostContext, services) =>
			{
				services.ConfigureInfrastructureServices(hostContext.Configuration, hostContext.HostingEnvironment);
				services.ConfigureApplicationServices();
				services.AddSingleton<ICurrentUserService, CurrentTestUserService>();
			});

		return hostBuilder.Start();
	}
}
