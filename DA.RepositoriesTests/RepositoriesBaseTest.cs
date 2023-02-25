using DA.BaseTests;
using DA.Repositories.Manager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace DA.RepositoriesTests;

[TestClass]
public abstract class RepositoriesBaseTest : BaseTestUnit
{
	private TransactionScope transactionScope = default!;
	public IRepositoryManager RepositoryManager { get; private set; } = default!;

	[TestInitialize]
	public override void Initialize()
	{
		transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
		base.Initialize();
	}

	[TestCleanup]
	public override void Cleanup()
	{
		base.Cleanup();
		transactionScope.Dispose();
	}
}
