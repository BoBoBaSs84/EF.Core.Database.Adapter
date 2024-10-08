﻿using Application.Contracts.Requests.Finance;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using Domain.Enumerators.Finance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace ApplicationTests.Validators.Contracts.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class CardUpdateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<CardUpdateRequest>? _validator;

	[TestMethod]
	public void RequestShouldBeValidWhenPropertiesAreCorrect()
	{
		_validator = CreateValidatorInstance();
		CardUpdateRequest request = RequestHelper.GetCardUpdateRequest();

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
		CardUpdateRequest request = new()
		{
			Type = (CardType)42,
			ValidUntil = DateTime.MaxValue
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().HaveCount(2);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Type));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.ValidUntil));
	}

	private static IValidator<CardUpdateRequest> CreateValidatorInstance()
		=> GetService<IValidator<CardUpdateRequest>>();
}