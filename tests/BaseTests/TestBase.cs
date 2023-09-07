using Application.Installer;
using Application.Interfaces.Presentation.Services;

using BaseTests.Helpers;
using BaseTests.Services;

using Domain.Constants;
using Domain.Entities.Identity;

using Infrastructure.Extensions;
using Infrastructure.Installer;
using Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BaseTests;

[TestClass]
public abstract class TestBase
{
	private static IHost s_host = default!;
	private static RepositoryContext s_context = default!;

	public static readonly ICollection<UserModel> Users = new List<UserModel>();

	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context)
	{
		if (s_host is not null)
			return;

		s_host = InitializeHost();
		
		s_context = GetRequiredService<RepositoryContext>();

		File.WriteAllText("SqlScript.sql", s_context.Database.GenerateCreateScript());

		s_context.Database.EnsureCreated();

		DataSeed();
	}

	[AssemblyCleanup]
	public static void AssemblyCleanup()
		=> s_context.Database.EnsureDeleted();

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

	private static void DataSeed()
	{
		DataSeedHelper.SeedCalendar();
		DataSeedHelper.SeedUsers();		
	}
}
