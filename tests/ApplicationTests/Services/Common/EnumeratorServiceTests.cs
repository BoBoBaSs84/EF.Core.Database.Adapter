using Application.Contracts.Responses.Common;
using Application.Interfaces.Infrastructure.Services;
using Application.Services.Common;

using AutoMapper;

using BaseTests.Helpers;

using Domain.Errors;

using FluentAssertions;

using Moq;

namespace ApplicationTests.Services.Common;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public sealed class EnumeratorServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<ILoggerService<EnumeratorService>> _loggerServiceMock = default!;

	[TestMethod]
	[TestCategory("Methods")]
	public void GetCardTypesTest()
	{
		EnumeratorService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<CardTypeResponse>> result = sut.GetCardTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Count.Should().Be(0);
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void GetDayTypesTest()
	{
		EnumeratorService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<AttendanceTypeResponse>> result = sut.GetAttendanceTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Count.Should().Be(0);
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void GetPriorityLevelTypesTest()
	{
		EnumeratorService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<PriorityLevelTypeResponse>> result = sut.GetPriorityLevelTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Count.Should().Be(0);
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void GetRoleTypesTest()
	{
		EnumeratorService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<RoleTypeResponse>> result = sut.GetRoleTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Count.Should().Be(0);
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public void GetWorkDayTypesTest()
	{
		EnumeratorService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<WorkDayTypeResponse>> result = sut.GetWorkDayTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Count.Should().Be(0);
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	private EnumeratorService CreateMockedInstance()
	{
		_loggerServiceMock = new();

		return new(_loggerServiceMock.Object, _mapper);
	}
}