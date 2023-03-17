using Application.Interfaces.Infrastructure.Identity;
using BaseTests;
using Infrastructure.Extensions;
using Infrastructure.Installer;
using Infrastructure.Persistence;
using InfrastructureTests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace InfrastructureTests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest - Not relevant here.")]
public class InfrastructureBaseTests : BaseTestUnit
{
	private static IHost testHost = default!;
	private const string Environment = Domain.Constants.Environment.Test;

	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context)
	{
		context.WriteLine($"{nameof(AssemblyInitialize)} ...");
		testHost = InitializeHost(context);

		ApplicationContext dbContext = GetRequiredService<ApplicationContext>();
		dbContext.Database.EnsureCreated();

		dbContext.CalendarDays.AddRange(EntityHelper.GetCalendarDays());
		dbContext.SaveChanges();
	}

	[AssemblyCleanup]
	public static void AssemblyCleanup()
	{
		ApplicationContext dbContext = GetRequiredService<ApplicationContext>();
		dbContext.Database.EnsureDeleted();
	}

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
			.ConfigureAppSettings(Environment)
			.UseEnvironment(Environment)
			.ConfigureServices((hostContext, services) =>
			{
				services.AddInfrastructureServices(hostContext.Configuration, hostContext.HostingEnvironment);
				services.TryAddSingleton<ICurrentUserService, CurrentTestUserService>();
			});

		return hostBuilder.Start();
	}
}
