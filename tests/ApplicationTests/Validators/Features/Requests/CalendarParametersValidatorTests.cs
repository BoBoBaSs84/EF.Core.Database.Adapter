using Application.Features.Requests;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using DRC = Application.Common.Constants.DateRanges;
using DRS = Application.Common.Statics.DateRanges;

namespace ApplicationTests.Validators.Features.Requests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, fluent assertions.")]
public sealed class CalendarParametersValidatorTests : ApplicationTestBase
{
	private IValidator<CalendarParameters> _validator = default!;

	[TestMethod]
	public void ParametersShouldBeValidWhenEverythingIsCorrect()
	{
		_validator = CreateValidatorInstance();
		CalendarParameters parameters = new()
		{
			Year = DRC.MinYear,
			Month = DRC.MinMonth,
			MinDate = DRS.MinDate,
			MaxDate = DRS.MaxDate
		};

		ValidationResult result = _validator.Validate(parameters);

		result.Should().NotBeNull();
		result.IsValid.Should().BeTrue();
		result.Errors.Should().BeEmpty();
	}

	[TestMethod]
	public void ParametersShouldBeInvalidWhenMinDateIsGreaterThanMaxDate()
	{
		_validator = CreateValidatorInstance();
		CalendarParameters parameters = new()
		{
			Year = DRC.MinYear,
			Month = DRC.MinMonth,
			MinDate = DRS.MaxDate,
			MaxDate = DRS.MinDate
		};

		ValidationResult result = _validator.Validate(parameters);

		result.Should().NotBeNull();
		result.IsValid.Should().BeFalse();
		result.Errors.Should().NotBeEmpty();
		result.Errors.Should().Contain(x => x.PropertyName == nameof(parameters.MinDate));
		result.Errors.Should().Contain(x => x.PropertyName == nameof(parameters.MaxDate));
	}

	private static IValidator<CalendarParameters> CreateValidatorInstance()
		=> GetService<IValidator<CalendarParameters>>();
}