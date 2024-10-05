using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Presentation.Services;

using Domain.Common;

using Infrastructure.Extensions;
using Infrastructure.Installer;
using Infrastructure.Persistence;

using InfrastructureTests.Helpers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace InfrastructureTests;

[TestClass]
public abstract class InfrastructureTestBase
{
	private static IServiceProvider? s_serviceProvider;
	private static IRepositoryContext? s_repositoryContext;

	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context)
	{
		IHost host = CreateHost();
		s_serviceProvider = host.Services;
		s_repositoryContext = host.Services.GetRequiredService<IRepositoryContext>();

		
		s_repositoryContext.Database.EnsureCreated();
		File.WriteAllText("CreateDatabase.sql", s_repositoryContext.Database.GenerateCreateScript());
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
			.ConfigureServices((context, services) =>
			{
				services.RegisterInfrastructureServices(context.Configuration, context.HostingEnvironment);
				services.TryAddSingleton<ICurrentUserService, TestUserService>();
			});

		return host.Start();
	}
}
