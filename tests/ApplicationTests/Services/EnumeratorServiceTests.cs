using Application.Contracts.Responses.Enumerators;
using Application.Interfaces.Application;

using BaseTests.Helpers;

using Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public sealed class EnumeratorServiceTests : ApplicationTestBase
{
	private readonly IEnumeratorService _enumeratorService;

	public EnumeratorServiceTests()
		=> _enumeratorService = GetRequiredService<IEnumeratorService>();

	[TestMethod]
	public void GetCardTypesSuccessTest()
	{
		ErrorOr<IEnumerable<CardTypeResponse>> cardTypes = _enumeratorService.GetCardTypes();

		AssertionHelper.AssertInScope(() =>
		{
			cardTypes.Should().NotBeNull();
			cardTypes.IsError.Should().BeFalse();
			cardTypes.Errors.Count.Should().Be(0);
			cardTypes.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	public void GetDayTypesSuccessTest()
	{
		ErrorOr<IEnumerable<AttendanceTypeResponse>> dayTypes = _enumeratorService.GetAttendanceTypes();

		AssertionHelper.AssertInScope(() =>
		{
			dayTypes.Should().NotBeNull();
			dayTypes.IsError.Should().BeFalse();
			dayTypes.Errors.Count.Should().Be(0);
			dayTypes.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	public void GetRoleTypesSuccessTest()
	{
		ErrorOr<IEnumerable<RoleTypeResponse>> roleTypes = _enumeratorService.GetRoleTypes();

		AssertionHelper.AssertInScope(() =>
		{
			roleTypes.Should().NotBeNull();
			roleTypes.IsError.Should().BeFalse();
			roleTypes.Errors.Count.Should().Be(0);
			roleTypes.Value.Should().NotBeNullOrEmpty();
		});
	}
}