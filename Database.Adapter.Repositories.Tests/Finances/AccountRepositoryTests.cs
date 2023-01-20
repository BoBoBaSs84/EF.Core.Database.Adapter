using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using static Database.Adapter.Entities.Constants;

namespace Database.Adapter.Repositories.Tests.Finances;

[TestClass()]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AccountRepositoryTests
{
	private TransactionScope transactionScope = default!;
	private IRepositoryManager repositoryManager = default!;

	[TestInitialize]
	public void TestInitialize()
	{
		transactionScope = new TransactionScope();
		repositoryManager = new RepositoryManager();
	}

	[TestCleanup]
	public void TestCleanup() => transactionScope.Dispose();

	[TestMethod()]
	public void GetAccountByIBANTest()
	{
		string iban = RandomHelper.GetString(Regex.IBAN);
		Account account = repositoryManager.AccountRepository.GetAccount(iban);
		account.Should().NotBeNull();
	}
}