using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
[SuppressMessage("Globalization", "CA1309", Justification = "Not supported.")]
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
	public void GetAllTest()
	{
		IQueryable<DayType> dayTypes = masterDataRepository.DayTypeRepository.GetAll();
		Assert.IsNotNull(dayTypes);
		Assert.IsTrue(dayTypes.Any());
	}

	[TestMethod]
	public void GetByIdTest()
	{
		int dayTypeId = 1;
		DayType dayType = masterDataRepository.DayTypeRepository.GetById(dayTypeId);
		Assert.IsNotNull(dayType);
		Assert.AreEqual(dayTypeId, dayType.Id);
	}

	[TestMethod]
	public void GetByConditionTest()
	{
		string dayTypeName = "Holiday";
		DayType dayType = masterDataRepository.DayTypeRepository.GetByCondition(x=>x.Name.Equals(dayTypeName));
		Assert.IsNotNull(dayType);
		Assert.AreEqual(dayTypeName, dayType.Name);
	}
	
	[TestMethod]
	public void GetManyByCondition()
	{
		IQueryable<DayType> dayTypes = masterDataRepository.DayTypeRepository.GetManyByCondition(x => x.IsActive.Equals(true));
		Assert.IsNotNull(dayTypes);
		Assert.IsTrue(dayTypes.Any());
	}

	[TestMethod]
	public void CreateTest()
	{
		DayType newDayType = GetDayType();
		masterDataRepository.DayTypeRepository.Create(newDayType);

		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);

		DayType dbDayType = masterDataRepository.DayTypeRepository.GetByCondition(x => x.Name.Equals(newDayType.Name));
		Assert.IsNotNull(dbDayType);
		Assert.AreEqual(dbDayType.Name, newDayType.Name);
	}

	[TestMethod]
	public void CreateRangeTest()
	{
		IEnumerable<DayType> newDayTypes = GetDayTypes();
		masterDataRepository.DayTypeRepository.CreateRange(newDayTypes);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(newDayTypes.Count(), commit);
	}

	[TestMethod]
	public void TrackChangesTest()
	{
		int dayTypeId = 1;
		DayType dayType = masterDataRepository.DayTypeRepository.GetById(dayTypeId);
		dayType.Description = GenerateRandomAlphanumericString(40);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void DeleteByEntityTest()
	{
		int dayTypeId = 1;
		DayType dayType = masterDataRepository.DayTypeRepository.GetById(dayTypeId);
		masterDataRepository.DayTypeRepository.Delete(dayType);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void DeleteById()
	{
		int dayTypeId = 1;
		masterDataRepository.DayTypeRepository.Delete(dayTypeId);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void UpdateTest()
	{

	}

	[TestMethod]
	public void UpdateRangeTest()
	{

	}

	private static DayType GetDayType() => new()
	{
		Name = GenerateRandomAlphanumericString(),
		Description = $"{GenerateRandomAlphanumericString} Description",
		IsActive = false
	};

	private static IEnumerable<DayType> GetDayTypes(int dayTypeToAdd = 0)
	{
		List<DayType> dayTypes = new();
		for (int i = 0; i <= dayTypeToAdd; i++)
			dayTypes.Add(GetDayType());
		return dayTypes;
	}

	private static string GenerateRandomAlphanumericString(int length = 10)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		Random random = new();
		string randomString = new(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
		return randomString;
	}
}
