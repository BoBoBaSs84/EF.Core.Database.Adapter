﻿using BB84.Home.Application.Contracts.Responses.Common;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Services.Common;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Errors;

using FluentAssertions;

namespace ApplicationTests.Services.Common;

public sealed partial class EnumeratorServiceTests
{
	[TestMethod]
	[TestCategory(nameof(EnumeratorService.GetAccountTypes))]
	public void GetAccountTypesShouldReturnResultWhenSuccessful()
	{
		EnumeratorService sut = CreateMockedInstance(_mapper);

		ErrorOr<IEnumerable<AccountTypeResponse>> result = sut.GetAccountTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
		});
	}

	[TestMethod]
	[TestCategory(nameof(EnumeratorService.GetAccountTypes))]
	public void GetAccountTypesShouldReturnFailedWhenExceptionGetThrown()
	{
		EnumeratorService sut = CreateMockedInstance(null!);

		ErrorOr<IEnumerable<AccountTypeResponse>> result = sut.GetAccountTypes();

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(EnumeratorServiceErrors.GetAccountTypesFailed);
			result.Value.Should().BeNullOrEmpty();
		});
	}
}
