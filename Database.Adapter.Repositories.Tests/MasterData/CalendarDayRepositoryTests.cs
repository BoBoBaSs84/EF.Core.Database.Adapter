using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using static Database.Adapter.Entities.Enumerators.DayType;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
public class CalendarDayRepositoryTests
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
		IQueryable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetAll();

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
	}

	[TestMethod]
	public void GetByConditionTest()
	{
		DateTime newDateTime = new(2020, 1, 1);
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetByCondition(x => x.Date.Equals(newDateTime));

		Assert.IsNotNull(dbCalendarDay);
		Assert.AreEqual(dbCalendarDay.Date, newDateTime);
	}

	[TestMethod]
	public void GetManyByConditionTest()
	{
		int calendarYear = 2020, calendarMonth = 12;
		IQueryable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
	}

	[TestMethod]
	public void GetByIdTest()
	{
		int calendarDayId = 1;
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetById(calendarDayId);

		Assert.IsNotNull(dbCalendarDay);
		Assert.AreEqual(dbCalendarDay.Id, calendarDayId);
	}

	[TestMethod]
	public void CreateTest()
	{
		CalendarDay calendarDay = GetCalendarDay();
		masterDataRepository.CalendarRepository.Create(calendarDay);
		int commit = masterDataRepository.CommitChanges();
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetByCondition(x=>x.Date.Equals(calendarDay.Date));

		Assert.AreEqual(1, commit);
		Assert.IsNotNull(dbCalendarDay);
		Assert.AreEqual(dbCalendarDay.Date, calendarDay.Date);
	}

	[TestMethod]
	public void CreateRangeTest()
	{
		IEnumerable<CalendarDay> calendarDays = GetCalendarDays(2);
		masterDataRepository.CalendarRepository.CreateRange(calendarDays);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(calendarDays.Count(), commit);
	}

	[TestMethod]
	public void DeleteByEntity()
	{
		int calendarDayId = 3;
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetById(calendarDayId);
		masterDataRepository.CalendarRepository.Delete(dbCalendarDay);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void DeleteById()
	{
		int calendarDayId = 3;
		masterDataRepository.CalendarRepository.Delete(calendarDayId);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void UpdateTest()
	{
		DateTime dateTime = new(2020, 1, 1);
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetByCondition(x => x.Date.Equals(dateTime));
		dbCalendarDay.Date = GetDateTime();
		masterDataRepository.CalendarRepository.Update(dbCalendarDay);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void UpdateRangeTest()
	{
		IQueryable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(x => x.Year.Equals(2020) && x.IsoWeek.Equals(3), true);
		foreach (CalendarDay dbCalendarDay in dbCalendarDays)
			dbCalendarDay.DayTypeId = 3;
		masterDataRepository.CalendarRepository.UpdateRange(dbCalendarDays);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(dbCalendarDays.Count(), commit);
	}

	[TestMethod]
	public void TrackChangesTest()
	{
		int calendarDayId = 1;
		CalendarDay calendarDay = masterDataRepository.CalendarRepository.GetById(calendarDayId);
		calendarDay.DayTypeId = 3;
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	private static DateTime GetDateTime(int dayToAdd = 0)
	{
		DateTime newDateTime = new(2099, 1, 1);
		return newDateTime.AddDays(dayToAdd);
	}

	private static CalendarDay GetCalendarDay(int dayToAdd = 0) => new()
	{
		Date = GetDateTime(dayToAdd),
		DayTypeId = (GetDateTime(dayToAdd).DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday) ? (int)WEEKENDDAY : (int)WEEKDAY
	};

	private static IEnumerable<CalendarDay> GetCalendarDays(int daysToAdd = 0)
	{
		List<CalendarDay> calendarDays = new();
		for (int i = 0; i <= daysToAdd; i++)
			calendarDays.Add(GetCalendarDay(i));
		return calendarDays;
	}
}
