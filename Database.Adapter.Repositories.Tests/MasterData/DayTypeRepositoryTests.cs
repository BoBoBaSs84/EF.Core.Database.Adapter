using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Context;
using Database.Adapter.Repositories.Context.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Database.Adapter.Base.Tests.Helpers;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DayTypeRepositoryTests
{
	private TransactionScope transactionScope = default!;
	private IMasterDataRepository masterDataRepository = default!;

	[TestInitialize]
	public void TestInitialize()
	{
		transactionScope = new TransactionScope();
		masterDataRepository = new MasterDataRepository();
	}

	[TestCleanup]
	public void TestCleanup() => transactionScope.Dispose();

	[TestMethod]
	public void GetByNameFailedTest()
	{
		string dayTypeName = RandomHelper.GetString(12);

		DayType dayType = masterDataRepository.DayTypeRepository.GetByName(dayTypeName);

		dayType.Should().BeNull();
	}

	[TestMethod]
	public void GetByNameSuccessTest()
	{
		string dayTypeName = "Holiday";

		DayType dayType = masterDataRepository.DayTypeRepository.GetByName(dayTypeName);

		dayType.Should().NotBeNull();
		dayType.Name.Should().Be(dayTypeName);
	}

	[TestMethod]
	public void GetAllActiveTest()
	{
		IEnumerable<DayType> dayTypes = masterDataRepository.DayTypeRepository.GetAllActive();
		dayTypes.Should().NotBeNullOrEmpty();
	}
}
