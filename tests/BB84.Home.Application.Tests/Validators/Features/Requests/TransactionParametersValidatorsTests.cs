﻿using BB84.Home.Application.Features.Requests;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using DRS = BB84.Home.Application.Common.ApplicationStatics.DateStatics;

namespace BB84.Home.Application.Tests.Validators.Features.Requests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class TransactionParametersValidatorsTests : ApplicationTestBase
{
	private IValidator<TransactionParameters> _validator = default!;

	[TestMethod]
	public void ParametersShouldBeValidWhenEverythingIsCorrect()
	{
		_validator = CreateValidatorInstance();
		TransactionParameters parameters = new()
		{
			BookingDate = DRS.MaxDate,
			ValueDate = DRS.MaxDate,
			Beneficiary = "Test",
			MinValue = 1M,
			MaxValue = 2M
		};

		ValidationResult result = _validator.Validate(parameters);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[TestMethod]
	public void ParametersShouldBeInvalidWhenMinValueIsGreaterThanMaxValue()
	{
		_validator = CreateValidatorInstance();
		TransactionParameters parameters = new()
		{
			BookingDate = DRS.MaxDate,
			ValueDate = DRS.MaxDate,
			Beneficiary = "Test",
			MinValue = 2M,
			MaxValue = 1M
		};

		ValidationResult result = _validator.Validate(parameters);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().Contain(x => x.PropertyName == nameof(parameters.MinValue));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(parameters.MaxValue));
	}

	private static IValidator<TransactionParameters> CreateValidatorInstance()
		=> GetService<IValidator<TransactionParameters>>();
}