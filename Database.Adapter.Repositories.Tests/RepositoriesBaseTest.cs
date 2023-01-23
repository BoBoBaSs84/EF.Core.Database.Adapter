using Database.Adapter.Base.Tests;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests;

[TestClass]
public abstract class RepositoriesBaseTest : BaseTest
{
	private TransactionScope transactionScope = default!;
	public IRepositoryManager RepositoryManager { get; private set; } = default!;

	[TestInitialize]
	public override void Initialize()
	{
		base.Initialize();
		transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
		RepositoryManager = new RepositoryManager();
	}

	[TestCleanup]
	public override void Cleanup()
	{
		base.Cleanup();
		transactionScope.Dispose();
	}
}
