using Application.Common.Interfaces;
using BaseTests.Helpers;
using Domain.Entities.Private;
using Domain.Enumerators;
using Domain.Extensions;
using FluentAssertions;
using static BaseTests.Constants;

namespace InfrastructureTests.Persistence.Repositories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class CalendarDayRepositoryTests : InfrastructureBaseTests
{
	private IUnitOfWork _unitOfWork = default!;
	private readonly int _year = DateTime.Today.Year;

	[TestMethod, Owner(Bobo)]
	public async Task GetAllAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		IEnumerable<CalendarDay> result = await _unitOfWork.CalendarDayRepository.GetAllAsync();

		result.Should().NotBeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByIdAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		int calendarDayId = 1;
		CalendarDay result = await _unitOfWork.CalendarDayRepository.GetByIdAsync(calendarDayId);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.Id.Should().Be(calendarDayId);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByIdsAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		IEnumerable<int> calendarDayIds = new[] { 1, 2 };
		IEnumerable<CalendarDay> result = await _unitOfWork.CalendarDayRepository.GetByIdsAsync(calendarDayIds);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNullOrEmpty();
			result.Should().HaveCount(calendarDayIds.Count());
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		DateTime date = RandomHelper.GetDateTime(_year);

		CalendarDay result = await _unitOfWork.CalendarDayRepository.GetByDateAsync(date);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.Year.Should().Be(date.Year);
			result.Month.Should().Be(date.Month);
			result.Day.Should().Be(date.Day);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDatesAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		IEnumerable<DateTime> dates = new List<DateTime>() { new(_year, 1, 1), new(_year, 1, 2) };

		IEnumerable<CalendarDay> result = await _unitOfWork.CalendarDayRepository.GetByDateAsync(dates);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNullOrEmpty();
			result.Should().HaveCount(dates.Count());
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateRangeAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		DateTime startDate = new(_year, 1, 1);
		DateTime endDate = startDate.AddDays(14);

		IEnumerable<CalendarDay> result = await _unitOfWork.CalendarDayRepository.GetByDateAsync(startDate, endDate);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNullOrEmpty();
			result.First().Date.Should().Be(startDate);
			result.Last().Date.Should().Be(endDate);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateTypeIdAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		int dayTypeId = (int)DayTypes.WEEKDAY;

		IEnumerable<CalendarDay> result = await _unitOfWork.CalendarDayRepository.GetByDayTypeAsync(dayTypeId);

		result.Should().NotBeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateTypeNameAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		string dayTypeName = DayTypes.WEEKDAY.GetName();

		IEnumerable<CalendarDay> result = await _unitOfWork.CalendarDayRepository.GetByDayTypeAsync(dayTypeName);

		result.Should().NotBeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByEndOfMonthAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		DateTime dateTime = DateTime.Now;

		IEnumerable<CalendarDay> result = await _unitOfWork.CalendarDayRepository.GetByEndOfMonthAsync(dateTime);

		result.Should().NotBeNullOrEmpty();
	}
}