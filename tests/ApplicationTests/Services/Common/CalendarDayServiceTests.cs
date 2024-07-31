using Application.Contracts.Responses.Common;
using Application.Interfaces.Application.Common;
using Application.Interfaces.Infrastructure.Logging;
using Application.Services.Common;

using AutoMapper;

using Domain.Errors;

using FluentAssertions;

using Moq;

namespace ApplicationTests.Services.Common;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit tests.")]
public sealed class CalendarDayServiceTests
{
	private Mock<IDateTimeService> _dateTimeServiceMock = default!;
	private Mock<ILoggerService<CalendarDayService>> _loggerServiceMock = default!;
	private Mock<IMapper> _mapperMock = default!;

	[TestMethod, TestCategory("Methods")]
	public void GetByDateShouldReturnResponseWhenSuccessfully()
	{
		CalendarDayService sut = CreateMockedInstance();
		DateTime date = DateTime.Today;

		ErrorOr<CalendarResponse> result = sut.GetByDate(date);

		result.Should().NotBeNull();
		result.Value.Date.Should().Be(date.Date);
	}

	[TestMethod, TestCategory("Methods")]
	public void GetPagedByParametersShouldReturnResponseWhenSuccessfully()
	{
		Assert.Fail();
	}

	[TestMethod, TestCategory("Methods")]
	public void GetCurrentShouldReturnResponseWhenSuccessfully()
	{
		DateTime today = DateTime.Today;
		CalendarDayService sut = CreateMockedInstance();
		_dateTimeServiceMock.Setup(x => x.Today).Returns(today);

		ErrorOr<CalendarResponse> result = sut.GetCurrent();

		result.Should().NotBeNull();
		result.Value.Date.Should().Be(today);
	}

	private CalendarDayService CreateMockedInstance()
	{
		_dateTimeServiceMock = new();
		_loggerServiceMock = new();
		_mapperMock = new();

		return new(_dateTimeServiceMock.Object, _loggerServiceMock.Object, _mapperMock.Object);
	}
}