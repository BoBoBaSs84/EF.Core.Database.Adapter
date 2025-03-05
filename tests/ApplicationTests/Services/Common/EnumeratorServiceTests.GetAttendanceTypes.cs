﻿using BaseTests.Helpers;

using BB84.Home.Application.Contracts.Responses.Common;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Services.Common;
using BB84.Home.Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services.Common;

public sealed partial class EnumeratorServiceTests
{
	[TestMethod]
	[TestCategory(nameof(EnumeratorService.GetAttendanceTypes))]
	public void GetAttendanceTypesShouldReturnResultWhenSuccessful()
	{
		EnumeratorService sut = CreateMockedInstance(_mapper);

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
	[TestCategory(nameof(EnumeratorService.GetAttendanceTypes))]
	public void GetAttendanceTypesShouldReturnFailedWhenExceptionGetThrown()
	{
		EnumeratorService sut = CreateMockedInstance(null!);

		ErrorOr<IEnumerable<AttendanceTypeResponse>> result = sut.GetAttendanceTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(EnumeratorServiceErrors.GetAttendanceTypesFailed);
			result.Value.Should().BeNullOrEmpty();
		});
	}
}
