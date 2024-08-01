using Domain.Constants;

using Infrastructure.Extensions;
using Infrastructure.Installer;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InfrastructureTests;

[TestClass]
public abstract class InfrastructureTestBase
{
	private static IServiceProvider? s_serviceProvider;

	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context)
	{
		IHost host = CreateHost();
		s_serviceProvider = host.Services;
	}

	[AssemblyCleanup]
	public static void AssemblyCleanup() { }

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
			.ConfigureServices((context, services) => services.ConfigureInfrastructureServices(context.Configuration, context.HostingEnvironment));

		return host.Start();
	}
}
