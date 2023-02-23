using DA.Models.Contexts.MasterData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;
using static DA.Models.Enumerators.DayType;

namespace DA.RepositoriesTests.BaseTypes;

[TestClass()]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class GenericRepositoryTests : RepositoriesBaseTest
{
	[TestMethod, Owner(Bobo)]
	public async Task CreateTest()
	{
		CalendarDay newCalendarDay = GetCalendarDay();

		await RepositoryManager.CalendarRepository.CreateAsync(newCalendarDay);
		await RepositoryManager.CommitChangesAsync();
		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Date.Equals(newCalendarDay.Date));

		AssertInScope(() =>
		{
			dbCalendarDay.Should().NotBeNull();
			dbCalendarDay.Date.Should().Be(newCalendarDay.Date);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task CreateManyTest()
	{
		IEnumerable<CalendarDay> calendarDays = GetCalendarDays(2);

		await RepositoryManager.CalendarRepository.CreateAsync(calendarDays);
		int commit = await RepositoryManager.CommitChangesAsync();

		commit.Should().Be(calendarDays.Count());
	}

	[TestMethod, Owner(Bobo)]
	public async Task DeleteByEntityTest()
	{
		int calendarDayId = 10;

		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);
		await RepositoryManager.CalendarRepository.DeleteAsync(dbCalendarDay);
		await RepositoryManager.CommitChangesAsync();
		dbCalendarDay = await RepositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().BeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task DeleteByIdTest()
	{
		int calendarDayId = 10;

		await RepositoryManager.CalendarRepository.DeleteAsync(calendarDayId);
		await RepositoryManager.CommitChangesAsync();
		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().BeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task DeleteByExpressionTest()
	{
		int calendarDayId = 10;

		await RepositoryManager.CalendarRepository.DeleteAsync(x => x.Id.Equals(calendarDayId));
		await RepositoryManager.CommitChangesAsync();
		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().BeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task DeleteManyTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));
		await RepositoryManager.CalendarRepository.DeleteAsync(dbCalendarDays);
		await RepositoryManager.CommitChangesAsync();
		dbCalendarDays = await RepositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));

		dbCalendarDays.Should().BeEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAllTest()
	{
		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetAllAsync();

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod()]
	public async Task GetManyByConditionTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));

		AssertInScope(() =>
		{
			dbCalendarDays.Should().NotBeNullOrEmpty();
			dbCalendarDays.First().Year.Should().Be(calendarYear);
			dbCalendarDays.First().Month.Should().Be(calendarMonth);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetManyByConditionWithOrderByDescendingTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderByDescending(x => x.Date));

		AssertInScope(() =>
		{
			dbCalendarDays.Should().NotBeNullOrEmpty();
			dbCalendarDays.First().Day.Should().Be(31);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetManyByConditionWithSkippingFirstTenTakingNextTenTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Date),
			top: 10,
			skip: 10);

		AssertInScope(() =>
		{
			dbCalendarDays.Should().NotBeNullOrEmpty();
			dbCalendarDays.Should().HaveCount(10);
			dbCalendarDays.First().Day.Should().Be(11);
			dbCalendarDays.Last().Day.Should().Be(20);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetManyByConditionWithTakingTwelveTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Date),
			top: 12);

		AssertInScope(() =>
		{
			dbCalendarDays.Should().NotBeNullOrEmpty();
			dbCalendarDays.Should().HaveCount(12);
			dbCalendarDays.First().Day.Should().Be(1);
			dbCalendarDays.Last().Day.Should().Be(12);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetManyByConditionWithIncludeTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Id),
			top: 1,
			includeProperties: new[] { nameof(CalendarDay.DayType) });

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().DayType.Should().NotBeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByIdTest()
	{
		int calendarDayId = 1;

		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Id.Should().Be(calendarDayId);
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByIdsTest()
	{
		IEnumerable<int> ids = new[] { 1, 2, 3, 4, 5, 6, 7 };

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetByIdsAsync(ids);

		dbCalendarDays.Should().NotBeNull();
		dbCalendarDays.Should().HaveCount(ids.Count());
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByConditionTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12,
			calenderDay = 6;

		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth) && x.Day.Equals(calenderDay));

		AssertInScope(() =>
		{
			dbCalendarDay.Should().NotBeNull();
			dbCalendarDay.Year.Should().Be(calendarYear);
			dbCalendarDay.Month.Should().Be(calendarMonth);
			dbCalendarDay.Day.Should().Be(calenderDay);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByConditionWithInclude()
	{
		int calendarYear = 2020,
			calendarMonth = 12,
			calenderDay = 24;

		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth) && x.Day.Equals(calenderDay),
			includeProperties: new[] { nameof(CalendarDay.DayType) }
			);

		AssertInScope(() =>
		{
			dbCalendarDay.Should().NotBeNull();
			dbCalendarDay.Year.Should().Be(calendarYear);
			dbCalendarDay.Month.Should().Be(calendarMonth);
			dbCalendarDay.Day.Should().Be(calenderDay);
			dbCalendarDay.DayType.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task UpdateTest()
	{
		int calendarDayId = 12;
		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Id.Equals(calendarDayId)
			);

		dbCalendarDay.Date = GetDateTime();
		await RepositoryManager.CalendarRepository.UpdateAsync(dbCalendarDay);
		await RepositoryManager.CommitChangesAsync();
		dbCalendarDay = await RepositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		AssertInScope(() =>
		{
			dbCalendarDay.Should().NotBeNull();
			dbCalendarDay.Date.Should().Be(GetDateTime());
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task UpdateManyTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));
		foreach (CalendarDay dbCalendarDay in dbCalendarDays.Where(x => x.Day.Equals(25) || x.Day.Equals(26)))
			dbCalendarDay.DayTypeId = (int)ABSENCE;
		await RepositoryManager.CalendarRepository.UpdateAsync(dbCalendarDays);
		int commit = await RepositoryManager.CommitChangesAsync();

		commit.Should().Be(31);
	}

	[TestMethod, Owner(Bobo)]
	public async Task UpdateTrackChangesTest()
	{
		int calendarDayId = 12;

		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Id.Equals(calendarDayId), trackChanges: true);
		dbCalendarDay.DayTypeId = (int)SICKNESS;
		await RepositoryManager.CommitChangesAsync();
		dbCalendarDay = await RepositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		AssertInScope(() =>
		{
			dbCalendarDay.Should().NotBeNull();
			dbCalendarDay.DayTypeId.Should().Be((int)SICKNESS);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task UpdateManyTrackChangesTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			trackChanges: true);
		foreach (CalendarDay dbCalendarDay in dbCalendarDays.Where(x => x.Day.Equals(25) || x.Day.Equals(26)))
			dbCalendarDay.DayTypeId = (int)BUISNESSTRIP;
		int commit = await RepositoryManager.CommitChangesAsync();

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