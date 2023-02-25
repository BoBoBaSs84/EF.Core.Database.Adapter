using DA.BaseTests;
using DA.Infrastructure.Installer;
using DA.Repositories.Installer;
using DA.Repositories.Manager.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace DA.RepositoriesTests;

[TestClass]
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
		RepositoryManager = GetRequiredService<IRepositoryManager>();
		transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
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

	private static T GetRequiredService<T>() =>
		(T)testHost.Services.GetRequiredService(typeof(T));
}
