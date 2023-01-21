using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Extensions;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DayTypeRepositoryTests
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

	[TestMethod]
	public async Task GetByNameFailedTest()
	{
		string dayTypeName = RandomHelper.GetString(12);

		DayType dayType = await repositoryManager.DayTypeRepository.GetByNameAsync(dayTypeName);

		dayType.Should().BeNull();
	}

	[TestMethod]
	public async Task GetByNameSuccessTest()
	{
		string dayTypeName = Entities.Enumerators.DayType.PLANNEDVACATION.GetName();

		DayType dayType = await repositoryManager.DayTypeRepository.GetByNameAsync(dayTypeName);

		dayType.Should().NotBeNull();
		dayType.Name.Should().Be(dayTypeName);
	}

	[TestMethod]
	public async Task GetAllActiveTest()
	{
		IEnumerable<DayType> dayTypes = await repositoryManager.DayTypeRepository.GetAllActiveAsync();
		dayTypes.Should().NotBeNullOrEmpty();
	}
}
