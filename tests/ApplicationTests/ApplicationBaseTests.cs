using Application.Installer;
using Application.Interfaces.Infrastructure.Identity;
using ApplicationTests.Helpers;
using BaseTests;
using Infrastructure.Extensions;
using Infrastructure.Installer;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Env = Domain.Constants.DomainConstants.Environment;

namespace ApplicationTests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest - Not relevant here.")]
public class ApplicationBaseTests : BaseTestUnit
{
	private static IHost testHost = default!;
	private const string Environment = Env.Testing;

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
				services.ConfigureInfrastructureServices(hostContext.Configuration, hostContext.HostingEnvironment);
				services.ConfigureApplicationServices();
				services.AddSingleton<ICurrentUserService, CurrentTestUserService>();
			});

		return hostBuilder.Start();
	}
}
