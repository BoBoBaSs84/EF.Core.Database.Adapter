using BB84.Home.Application.Contracts.Responses.Common;
using BB84.Home.Application.Services.Common;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services.Common;

public sealed partial class EnumeratorServiceTests
{
	[TestMethod]
	[TestCategory(nameof(EnumeratorService.GetWorkDayTypes))]
	public void GetWorkDayTypesShouldReturnResultWhenSuccessful()
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

	[TestMethod]
	[TestCategory(nameof(EnumeratorService.GetWorkDayTypes))]
	public void GetWorkDayTypesShouldReturnFailedWhenExceptionGetThrown()
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
}
