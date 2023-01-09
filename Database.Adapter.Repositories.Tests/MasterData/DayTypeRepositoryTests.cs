using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
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
	public void TestCleanup()
	{
		transactionScope.Dispose();
	}

	[TestMethod]
	public void GetAllTest()
	{
		IQueryable<DayType> dayTypes = masterDataRepository.DayTypeRepository.GetAll();
		Assert.IsNotNull(dayTypes);
		Assert.IsTrue(dayTypes.Any());
	}

	[TestMethod]
	public void GetAllActiveTest()
	{
		IQueryable<DayType> dayTypes = masterDataRepository.DayTypeRepository.GetAllActive();
		Assert.IsNotNull(dayTypes);
		Assert.IsTrue(dayTypes.Any());
	}

	[DataTestMethod]
	[DataRow(1)]
	[DataRow(2)]
	[DataRow(3)]
	public void GetByIdTest(int dayTypeId)
	{
		DayType dayType = masterDataRepository.DayTypeRepository.GetById(dayTypeId);
		Assert.IsNotNull(dayType);
		Assert.AreEqual(dayType.Id, dayTypeId);
	}

	[DataTestMethod]
	[DataRow("Absence")]
	[DataRow("Weekday")]
	[DataRow("Holiday")]
	public void GetByNameTest(string dayTypeName)
	{
		DayType dayType = masterDataRepository.DayTypeRepository.GetByName(dayTypeName);
		Assert.IsNotNull(dayType);
		Assert.AreEqual(dayType.Name, dayTypeName);
	}

	[TestMethod]
	public void CreateTest()
	{
		DayType newDayType = new() { Name = "UnitTest", Description = "DayType UnitTest", IsActive = false };
		masterDataRepository.DayTypeRepository.Create(newDayType);

		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);

		DayType dbDayType = masterDataRepository.DayTypeRepository.GetByName(newDayType.Name);
		Assert.IsNotNull(dbDayType);
		Assert.AreEqual(dbDayType.Name, newDayType.Name);
	}

	[TestMethod]
	public void CreateRangeTest()
	{
		IEnumerable<DayType> newDayTypes = new List<DayType>()
		{
			new() { Name = "UnitTestOne", Description = "DayType UnitTestOne", IsActive = false, Id = 1234 },
			new() { Name = "UnitTestTwo", Description = "DayType UnitTestTwo", IsActive = false, Id = 1235 },
		};

		masterDataRepository.DayTypeRepository.CreateRange(newDayTypes);

		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(newDayTypes.Count(), commit);

		IQueryable<DayType> dbDayTypes = masterDataRepository.DayTypeRepository.GetManyByCondition(x => x.Name.Contains("UnitTest"));
		Assert.IsNotNull(dbDayTypes);
		Assert.AreEqual(dbDayTypes.Count(), newDayTypes.Count());
	}
}
