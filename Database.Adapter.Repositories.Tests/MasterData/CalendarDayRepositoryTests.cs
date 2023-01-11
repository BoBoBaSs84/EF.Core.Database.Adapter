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
		IEnumerable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetAll();

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
		IEnumerable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
	}

	[TestMethod]
	public void GetManyByConditionWithOrderByDescendingTest()
	{
		int calendarYear = 2020, calendarMonth = 12;
		IList<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderByDescending(x => x.Date)
			).ToList();

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
		Assert.AreEqual(31, dbCalendarDays[0].Day);
	}

	[TestMethod]
	public void GetManyByConditionWithSkippingFirstTenTakingNextTenTest()
	{
		int calendarYear = 2020, calendarMonth = 12;
		IList<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Date),
			top: 10,
			skip: 10
			).ToList();

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
		Assert.AreEqual(dbCalendarDays.Count, 10);
		Assert.AreEqual(dbCalendarDays[0].Day, 11);
		Assert.AreEqual(dbCalendarDays[9].Day, 20);
	}

	[TestMethod]
	public void GetManyByConditionWithTakingTwelveTest()
	{
		int calendarYear = 2021, calendarMonth = 02;
		IList<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Date),
			top: 12
			).ToList();

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
		Assert.AreEqual(dbCalendarDays.Count, 12);
		Assert.AreEqual(dbCalendarDays[0].Day, 1);
		Assert.AreEqual(dbCalendarDays[11].Day, 12);
	}

	[TestMethod]
	public void GetManyByConditionWithIncludeTest()
	{
		int calendarYear = 2020, calendarMonth = 12;
		IList<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Id),
			top: 1,
			includeProperties: new[] { nameof(CalendarDay.DayType) }
			).ToList();

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
		Assert.AreEqual(dbCalendarDays.Count, 1);
		Assert.IsNotNull(dbCalendarDays[0].DayType);
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
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetByCondition(x => x.Date.Equals(calendarDay.Date));

		Assert.AreEqual(1, commit);
		Assert.IsNotNull(dbCalendarDay);
		Assert.AreEqual(dbCalendarDay.Date, calendarDay.Date);
	}

	[TestMethod]
	public void CreateRangeTest()
	{
		IEnumerable<CalendarDay> calendarDays = GetCalendarDays(2);
		masterDataRepository.CalendarRepository.Create(calendarDays);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(calendarDays.Count(), commit);
	}

	[TestMethod]
	public void DeleteByEntityTest()
	{
		int calendarDayId = 3;
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetById(calendarDayId);
		masterDataRepository.CalendarRepository.Delete(dbCalendarDay);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void DeleteByIdTest()
	{
		int calendarDayId = 3;
		masterDataRepository.CalendarRepository.Delete(calendarDayId);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void DeleteByExpressionTest()
	{
		int calendarDayId = 9;
		masterDataRepository.CalendarRepository.Delete(x => x.Id.Equals(calendarDayId));
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void DeleteRangeTest()
	{
		IEnumerable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(x => x.Id <= 2);
		masterDataRepository.CalendarRepository.Delete(dbCalendarDays);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(2, commit);
	}

	[TestMethod]
	public void UpdateTest()
	{
		int calendarDayId = 7;
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetById(calendarDayId);
		dbCalendarDay.DayTypeId = 3;
		masterDataRepository.CalendarRepository.Update(dbCalendarDay);
		int commit = masterDataRepository.CommitChanges();
		Assert.AreEqual(1, commit);
	}

	[TestMethod]
	public void UpdateRangeTest()
	{
		IEnumerable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(x => x.Year.Equals(2020) && x.IsoWeek.Equals(3));
		foreach (CalendarDay dbCalendarDay in dbCalendarDays)
			dbCalendarDay.DayTypeId = 3;
		masterDataRepository.CalendarRepository.Update(dbCalendarDays);
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
