﻿using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Tests.Helpers;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Enumerators.Finance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace BB84.Home.Application.Tests.Validators.Contracts.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class AccountUpdateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<AccountUpdateRequest>? _validator;

	[TestMethod]
	public void RequestShouldBeValidWhenPropertiesAreCorrect()
	{
		_validator = CreateValidatorInstance();
		AccountUpdateRequest request = RequestHelper.GetAccountUpdateRequest();

		ValidationResult result = _validator.Validate(request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeEmpty();
		});
	}

	[TestMethod]
	public void RequestShouldNotBeValidWhenPropertiesAreNotCorrect()
	{
		_validator = CreateValidatorInstance();
		AccountUpdateRequest request = new()
		{
			Provider = RandomHelper.GetString(501),
			Type = (AccountType)42
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().HaveCount(2);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Provider));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Type));
	}

	private static IValidator<AccountUpdateRequest> CreateValidatorInstance()
		=> GetService<IValidator<AccountUpdateRequest>>();
}