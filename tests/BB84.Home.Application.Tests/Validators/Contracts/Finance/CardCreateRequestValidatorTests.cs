﻿using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Tests.Helpers;
using BB84.Home.Base.Tests.Constants;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Enumerators.Finance;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

namespace BB84.Home.Application.Tests.Validators.Contracts.Finance;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class CardCreateRequestValidatorTests : ApplicationTestBase
{
	private IValidator<CardCreateRequest>? _validator;

	[TestMethod]
	public void RequestShouldBeValidWhenPropertiesAreCorrect()
	{
		_validator = CreateValidatorInstance();
		CardCreateRequest request = RequestHelper.GetCardCreateRequest();

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
		CardCreateRequest request = new()
		{
			PAN = RandomHelper.GetString(20, TestConstants.WildCardChars),
			Type = (CardType)42,
			ValidUntil = DateTime.MaxValue
		};

		ValidationResult result = _validator.Validate(request);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().HaveCount(3);
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.PAN));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.Type));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(request.ValidUntil));
	}

	private static IValidator<CardCreateRequest> CreateValidatorInstance()
		=> GetService<IValidator<CardCreateRequest>>();
}