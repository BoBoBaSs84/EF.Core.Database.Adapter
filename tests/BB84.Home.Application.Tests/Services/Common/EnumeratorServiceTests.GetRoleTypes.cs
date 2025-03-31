using BB84.Home.Application.Contracts.Responses.Common;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Services.Common;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services.Common;
public sealed partial class EnumeratorServiceTests
{
	[TestMethod]
	[TestCategory(nameof(EnumeratorService.GetRoleTypes))]
	public void GetRoleTypesShouldReturnResultWhenSuccessful()
	{
		EnumeratorService sut = CreateMockedInstance(_mapper);

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
	[TestCategory(nameof(EnumeratorService.GetRoleTypes))]
	public void GetRoleTypesShouldReturnFailedWhenExceptionGetThrown()
	{
		EnumeratorService sut = CreateMockedInstance(null!);

		ErrorOr<IEnumerable<RoleTypeResponse>> result = sut.GetRoleTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(EnumeratorServiceErrors.GetRoleTypesFailed);
			result.Value.Should().BeNullOrEmpty();
		});
	}
}
