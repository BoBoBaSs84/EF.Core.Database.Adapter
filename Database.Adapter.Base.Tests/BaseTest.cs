using Database.Adapter.Repositories;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace Database.Adapter.Base.Tests;

[TestClass]
public abstract class BaseTest
{
	private TransactionScope transactionScope = default!;
	public IRepositoryManager RepositoryManager { get; private set; } = default!;

	[TestInitialize]
	public virtual void TestInitialize()
	{
		transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
		RepositoryManager = new RepositoryManager();
	}

	[TestCleanup]
	public virtual void TestCleanup() =>
		transactionScope.Dispose();
}
