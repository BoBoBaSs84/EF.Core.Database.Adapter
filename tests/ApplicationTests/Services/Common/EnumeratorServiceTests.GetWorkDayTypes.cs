using Application.Contracts.Responses.Common;
using Application.Errors.Services;
using Application.Services.Common;

using BaseTests.Helpers;

using BB84.Home.Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services.Common;

public sealed partial class EnumeratorServiceTests
{
	[TestMethod]
	[TestCategory(nameof(EnumeratorService.GetWorkDayTypes))]
	public void GetWorkDayTypesShouldReturnResultWhenSuccessful()
	{
		EnumeratorService sut = CreateMockedInstance(_mapper);

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
		EnumeratorService sut = CreateMockedInstance(null!);

		ErrorOr<IEnumerable<WorkDayTypeResponse>> result = sut.GetWorkDayTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(EnumeratorServiceErrors.GetWorkDayTypesFailed);
			result.Value.Should().BeNullOrEmpty();
		});
	}
}
