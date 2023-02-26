using DA.BaseTests;
using DA.Infrastructure.Installer;
using DA.Repositories.Installer;
using DA.Repositories.Manager.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace DA.RepositoriesTests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest - Not relevant here.")]
public abstract class RepositoriesBaseTest : BaseTestUnit
{
	private TransactionScope transactionScope = default!;
	private static IHost testHost = default!;

	public IRepositoryManager RepositoryManager { get; private set; } = default!;

	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context) =>
		testHost = InitializeHost(context);

	[TestInitialize]
	public override void Initialize()
	{
		transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
		RepositoryManager = GetRequiredService<IRepositoryManager>();		
		base.Initialize();
	}

	[TestCleanup]
	public override void Cleanup()
	{
		base.Cleanup();
		transactionScope.Dispose();
	}

	private static IHost InitializeHost(TestContext context)
	{
		context.WriteLine($"{nameof(InitializeHost)} ...");

		IHostBuilder hostBuilder = Host.CreateDefaultBuilder(new[] { string.Empty })
			.ConfigureServices((hostContext, services) =>
			{
				services.AddInfrastructureServices();
				services.AddRepositoryManager();
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
