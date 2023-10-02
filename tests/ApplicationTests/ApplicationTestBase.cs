using Application.Installer;
using Application.Interfaces.Presentation.Services;

using BaseTests.Services;

using Domain.Constants;
using Domain.Models.Identity;

using Infrastructure.Extensions;
using Infrastructure.Installer;
using Infrastructure.Persistence;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationTests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class ApplicationTestBase
{
	private static IServiceProvider? s_serviceProvider;
	private static RepositoryContext? s_repositoryContext;

	public static IList<UserModel> Users { get; private set; } = new List<UserModel>();
	public static IList<RoleModel> Roles { get; private set; } = new List<RoleModel>();


	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context)
	{
		IHost host = CreateHost();

		s_serviceProvider = host.Services;
		s_repositoryContext = host.Services.GetRequiredService<RepositoryContext>();

		s_repositoryContext.Database.EnsureCreated();

		DataSeed();
	}

	[AssemblyCleanup]
	public static void AssemblyCleanup()
		=> s_repositoryContext?.Database.EnsureDeleted();

	/// <summary>
	/// Returns the requested service if it was registered within the service collection.
	/// </summary>
	/// <typeparam name="TService">The type of service we are operating on.</typeparam>
	/// <returns>The requested service.</returns>
	public static T GetService<T>()
		=> s_serviceProvider?.GetRequiredService(typeof(T)) is not T service
		? throw new ArgumentException($"{typeof(T)} needs to be a registered service.")
		: service;

	private static IHost CreateHost()
	{
		string env = DomainConstants.Environment.Testing;

		IHostBuilder host = Host.CreateDefaultBuilder()
			.ConfigureAppSettings(env)
			.UseEnvironment(env)
			.ConfigureServices((hostContext, services) =>
			{
				services.ConfigureInfrastructureServices(hostContext.Configuration, hostContext.HostingEnvironment);
				services.ConfigureApplicationServices();
				services.AddSingleton<ICurrentUserService, CurrentTestUserService>();
			});

		return host.Start();
	}

	private static void DataSeed()
	{
		DataSeedHelper.SeedCalendar();
		DataSeedHelper.SeedUsers();
	}
}
