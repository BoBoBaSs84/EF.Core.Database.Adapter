using DA.Base.Tests;
using DA.Repositories.Manager;
using DA.Repositories.Manager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace DA.Repositories.Tests;

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
