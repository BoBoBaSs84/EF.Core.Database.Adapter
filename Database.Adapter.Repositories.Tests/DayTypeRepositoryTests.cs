using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests;

[TestClass]
public class DayTypeRepositoryTests
{
	private TransactionScope transactionScope = default!;
	private IMasterDataRepository masterDataRepository = default!;
	private readonly DayType dayType = new() { Name = "UnitTest", Description = "DayType UnitTest", IsActive = true };

	[TestInitialize]
	public void TestInitialize()
	{
		transactionScope = new TransactionScope();
		masterDataRepository = new MasterDataRepository();
	}

	[TestCleanup]
	public void TestCleanup() => transactionScope.Dispose();

	[TestMethod]
	public void DayTypeRepositoryGetAllTest()
	{
		IQueryable<DayType> dayTypes = masterDataRepository.DayTypeRepository.GetAll();
		Assert.IsNotNull(dayTypes);
		Assert.IsTrue(dayTypes.Any());
	}

	[TestMethod]
	public void DayTypeRepositoryGetAllActiveTest()
	{
		IQueryable<DayType> dayTypes = masterDataRepository.DayTypeRepository.GetAllActive();
		Assert.IsNotNull(dayTypes);
		Assert.IsTrue(dayTypes.Any());
	}

	[DataTestMethod]
	[DataRow(1)]
	[DataRow(2)]
	[DataRow(3)]
	public void DayTypeRepositoryGetByIdTest(int dayTypeId)
	{
		DayType dayType = masterDataRepository.DayTypeRepository.GetById(dayTypeId);
		Assert.IsNotNull(dayType);
		Assert.AreEqual(dayType.Id, dayTypeId);
	}

	[DataTestMethod]
	[DataRow("Absence")]
	[DataRow("Weekday")]
	[DataRow("Holiday")]
	public void DayTypeRepositoryGetByNameTest(string dayTypeName)
	{
		DayType dayType = masterDataRepository.DayTypeRepository.GetByName(dayTypeName);
		Assert.IsNotNull(dayType);
		Assert.AreEqual(dayType.Name, dayTypeName);
	}

	[TestMethod]
	public void DayTypeRepositoryCreateTest()
	{
		DayType newDayType = new() { Name = "UnitTest", Description = "DayType UnitTest", IsActive = true };
		masterDataRepository.DayTypeRepository.Create(newDayType);

		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);

		DayType dbDayType = masterDataRepository.DayTypeRepository.GetByName(newDayType.Name);
		Assert.IsNotNull(dbDayType);
		Assert.AreEqual(dbDayType.Name, newDayType.Name);
	}
}
