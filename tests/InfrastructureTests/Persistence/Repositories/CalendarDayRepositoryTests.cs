using Application.Interfaces.Infrastructure;
using BaseTests.Helpers;
using Domain.Entities.Private;
using FluentAssertions;
using static BaseTests.Constants.TestConstants;

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
		CalendarDay? result = await _unitOfWork.CalendarDayRepository.GetByIdAsync(calendarDayId);

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
}
