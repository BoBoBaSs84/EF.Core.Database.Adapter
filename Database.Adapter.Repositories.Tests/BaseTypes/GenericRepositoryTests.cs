using Database.Adapter.Entities.Contexts.Application.MasterData;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using static Database.Adapter.Entities.Enumerators.DayType;

namespace Database.Adapter.Repositories.Tests.BaseTypes;

[TestClass()]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class GenericRepositoryTests
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
	public void CreateTest()
	{
		CalendarDay newCalendarDay = GetCalendarDay();

		repositoryManager.CalendarRepository.Create(newCalendarDay);
		repositoryManager.CommitChanges();
		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetByCondition(x => x.Date.Equals(newCalendarDay.Date));

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Date.Should().Be(newCalendarDay.Date);
	}

	[TestMethod]
	public void CreateManyTest()
	{
		IEnumerable<CalendarDay> calendarDays = GetCalendarDays(2);

		repositoryManager.CalendarRepository.Create(calendarDays);
		int commit = repositoryManager.CommitChanges();

		commit.Should().Be(calendarDays.Count());
	}

	[TestMethod]
	public void DeleteByEntityTest()
	{
		int calendarDayId = 10;

		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetById(calendarDayId);
		repositoryManager.CalendarRepository.Delete(dbCalendarDay);
		repositoryManager.CommitChanges();
		dbCalendarDay = repositoryManager.CalendarRepository.GetById(calendarDayId);

		dbCalendarDay.Should().BeNull();
	}

	[TestMethod]
	public void DeleteByIdTest()
	{
		int calendarDayId = 10;

		repositoryManager.CalendarRepository.Delete(calendarDayId);
		repositoryManager.CommitChanges();
		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetById(calendarDayId);

		dbCalendarDay.Should().BeNull();
	}

	[TestMethod]
	public void DeleteByExpressionTest()
	{
		int calendarDayId = 10;

		repositoryManager.CalendarRepository.Delete(x => x.Id.Equals(calendarDayId));
		repositoryManager.CommitChanges();
		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetById(calendarDayId);

		dbCalendarDay.Should().BeNull();
	}

	[TestMethod]
	public void DeleteManyTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth)
			);
		repositoryManager.CalendarRepository.Delete(dbCalendarDays);
		repositoryManager.CommitChanges();
		dbCalendarDays = repositoryManager.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth)
			);

		dbCalendarDays.Should().BeEmpty();
	}

	[TestMethod]
	public void GetAllTest()
	{
		IEnumerable<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetAll();

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod()]
	public void GetManyByConditionTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth)
			);

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().Year.Should().Be(calendarYear);
		dbCalendarDays.First().Month.Should().Be(calendarMonth);
	}

	[TestMethod]
	public void GetManyByConditionWithOrderByDescendingTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IList<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderByDescending(x => x.Date)
			).ToList();

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().Day.Should().Be(31);
	}

	[TestMethod]
	public void GetManyByConditionWithSkippingFirstTenTakingNextTenTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IList<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Date),
			top: 10,
			skip: 10
			).ToList();

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.Should().HaveCount(10);
		dbCalendarDays.First().Day.Should().Be(11);
		dbCalendarDays.Last().Day.Should().Be(20);
	}

	[TestMethod]
	public void GetManyByConditionWithTakingTwelveTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IList<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Date),
			top: 12
			).ToList();

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.Should().HaveCount(12);
		dbCalendarDays.First().Day.Should().Be(1);
		dbCalendarDays.Last().Day.Should().Be(12);
	}

	[TestMethod]
	public void GetManyByConditionWithIncludeTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IList<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Id),
			top: 1,
			includeProperties: new[] { nameof(CalendarDay.DayType) }
			).ToList();

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().DayType.Should().NotBeNull();
	}

	[TestMethod]
	public void GetByIdTest()
	{
		int calendarDayId = 1;

		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetById(calendarDayId);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Id.Should().Be(calendarDayId);
	}

	[TestMethod]
	public void GetByConditionTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12,
			calenderDay = 6;

		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth) && x.Day.Equals(calenderDay)
			);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Year.Should().Be(calendarYear);
		dbCalendarDay.Month.Should().Be(calendarMonth);
		dbCalendarDay.Day.Should().Be(calenderDay);
	}

	[TestMethod]
	public void GetByConditionWithInclude()
	{
		int calendarYear = 2020,
			calendarMonth = 12,
			calenderDay = 24;

		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth) && x.Day.Equals(calenderDay),
			includeProperties: new[] { nameof(CalendarDay.DayType) }
			);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Year.Should().Be(calendarYear);
		dbCalendarDay.Month.Should().Be(calendarMonth);
		dbCalendarDay.Day.Should().Be(calenderDay);
		dbCalendarDay.DayType.Should().NotBeNull();
	}

	[TestMethod]
	public void UpdateTest()
	{
		int calendarDayId = 12;
		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetByCondition(
			expression: x => x.Id.Equals(calendarDayId)
			);

		dbCalendarDay.Date = GetDateTime();
		repositoryManager.CalendarRepository.Update(dbCalendarDay);
		repositoryManager.CommitChanges();
		dbCalendarDay = repositoryManager.CalendarRepository.GetById(calendarDayId);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Date.Should().Be(GetDateTime());
	}

	[TestMethod]
	public void UpdateManyTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth)
			);
		foreach (CalendarDay dbCalendarDay in dbCalendarDays.Where(x => x.Day.Equals(25) || x.Day.Equals(26)))
			dbCalendarDay.DayTypeId = (int)HOLIDAY;
		repositoryManager.CalendarRepository.Update(dbCalendarDays);
		int commit = repositoryManager.CommitChanges();

		commit.Should().Be(31);
	}

	[TestMethod]
	public void UpdateTrackChangesTest()
	{
		int calendarDayId = 12;

		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetByCondition(
			expression: x => x.Id.Equals(calendarDayId),
			trackChanges: true
			);
		dbCalendarDay.DayTypeId = (int)HOLIDAY;
		repositoryManager.CommitChanges();
		dbCalendarDay = repositoryManager.CalendarRepository.GetById(calendarDayId);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.DayTypeId.Should().Be((int)HOLIDAY);
	}

	[TestMethod]
	public void UpdateManyTrackChangesTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetManyByCondition(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			trackChanges: true
			);
		foreach (CalendarDay dbCalendarDay in dbCalendarDays.Where(x => x.Day.Equals(25) || x.Day.Equals(26)))
			dbCalendarDay.DayTypeId = (int)HOLIDAY;
		int commit = repositoryManager.CommitChanges();

		commit.Should().Be(2);
	}

	private static DateTime GetDateTime(int dayToAdd = 0)
	{
		DateTime newDateTime = new(2199, 1, 1);
		return newDateTime.AddDays(dayToAdd);
	}

	private static CalendarDay GetCalendarDay(int dayToAdd = 0) => new()
	{
		Date = GetDateTime(dayToAdd),
		DayTypeId = GetDateTime(dayToAdd).DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday ? (int)WEEKENDDAY : (int)WEEKDAY
	};

	private static IEnumerable<CalendarDay> GetCalendarDays(int daysToAdd = 0)
	{
		List<CalendarDay> calendarDays = new();
		for (int i = 0; i <= daysToAdd; i++)
			calendarDays.Add(GetCalendarDay(i));
		return calendarDays;
	}
}